using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace SurvivorsLike
{
    public partial struct PlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            InputManager inputs = SystemAPI.GetSingleton<InputSingleton>().Input.Value;

            Move(ref state, inputs);
            Scope(ref state, inputs);
            Shoot(ref state, inputs);
            SelectWeapon(ref state, inputs);            
        }

        private void Move(ref SystemState state, InputManager inputs)
        {
            float2 moveInput = inputs.Move();
            foreach (var movement in SystemAPI.Query<RefRW<Movement>>().WithAll<PlayerTag>())
            {
                movement.ValueRW.Direction = moveInput;
            }
        }

        private void Scope(ref SystemState state, InputManager inputs)
        {
            float3 scopeInput = inputs.Scope();
            foreach (var (anim, transform) in SystemAPI.Query<RefRW<Animated>, LocalToWorld>().WithAll<PlayerTag>())
            {
                float3 entityPosition = transform.Position;
                anim.ValueRW.Animator.Value.SetRotation((int)math.sign(scopeInput.x - entityPosition.x));
                anim.ValueRW.WeaponAnimation.Value.Scope(scopeInput.x, scopeInput.y);
            }
        }

        private void Shoot(ref SystemState state, InputManager inputs)
        {
            float3 scopeInput = inputs.Scope();

            BeginInitializationEntityCommandBufferSystem.Singleton ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            var elapsedTime = SystemAPI.Time.ElapsedTime;

            foreach (var (weapon, transform, timestamp, entity) in
                    SystemAPI.Query<Weapon, LocalToWorld, RefRW<WeaponCooldownTimestamp>>().WithEntityAccess())
            {
                if (timestamp.ValueRO.Value > elapsedTime) continue;

                if (inputs.Shoot())
                {
                    timestamp.ValueRW.Value = elapsedTime + weapon.Cooldown;

                    int bulletCount = 1;
                    float baseAngle = 0;
                    float step = 0;

                    if (SystemAPI.HasComponent<WeaponRange>(entity))
                    {
                        WeaponRange range = SystemAPI.GetComponent<WeaponRange>(entity);
                        bulletCount = range.BulletCount;
                        baseAngle = range.Angle / 2;
                        step = range.Angle / math.max(bulletCount - 1, 1);
                    }

                    float3 startDirection = math.normalize(scopeInput - transform.Position);

                    for (int i = 0; i < bulletCount; i++)
                    {
                        float currentAngle = i * step - baseAngle;
                        var rotation = quaternion.Euler(0f, 0f, math.radians(currentAngle));
                        float2 direction = math.mul(rotation, startDirection).xy;

                        Entity newBullet = ecb.Instantiate(weapon.BulletPrefab);

                        ecb.SetComponent(newBullet, new Movement
                        {
                            Direction = direction,
                            Speed = weapon.BulletSpeed
                        });
                        ecb.SetComponent(newBullet, new PlayerAttack
                        {
                            HitPoints = weapon.BulletDamage
                        });
                        ecb.SetComponent(newBullet, LocalTransform.FromPosition(transform.Position));
                    }
                }
            }
        }

        private void SelectWeapon(ref SystemState state, InputManager inputs)
        {
            if (inputs.Selector() > 0)
            {
                foreach (var (arsenal, weaponBuffer, weapon, range, player) in
                    SystemAPI.Query<RefRW<WeaponArsenal>, DynamicBuffer<WeaponDataElement>, RefRW<Weapon>, RefRW<WeaponRange>, Parent>())
                {
                    int index = inputs.Selector() - 1;
                    if (arsenal.ValueRO.CurrentWeapon == index) continue;
                    arsenal.ValueRW.CurrentWeapon = index;

                    WeaponDataElement data = weaponBuffer[index];
                    weapon.ValueRW.BulletDamage = data.BulletDamage;
                    weapon.ValueRW.BulletSpeed = data.BulletSpeed;
                    weapon.ValueRW.Cooldown = data.Cooldown;

                    range.ValueRW.BulletCount = data.BulletCount;
                    range.ValueRW.Angle = data.Angle;

                    if (SystemAPI.HasComponent<Animated>(player.Value))
                    {
                        Animated animated = SystemAPI.GetComponent<Animated>(player.Value);
                        animated.WeaponAnimation.Value.SetSprite(index);
                    }
                }
            }
        }
    }
}
