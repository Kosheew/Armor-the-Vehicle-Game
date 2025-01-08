namespace Character.Damage
{
    public class DamageHandler : IDamageHandler
    {
        private int _damage;

        public DamageHandler(int damage)
        {
            _damage = damage;
        }

        public void HandleDamage(IDamagable damagable)
        {
            damagable.TakeDamage(_damage);
        }
    }
}