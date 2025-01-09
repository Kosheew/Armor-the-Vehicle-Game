namespace Handlers.Damage
{
    public class DamageHandler : IDamageHandler
    {
        private readonly int _damage;

        public DamageHandler(int damage)
        {
            _damage = damage;
        }

        public void HandleDamage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
        }
    }
}