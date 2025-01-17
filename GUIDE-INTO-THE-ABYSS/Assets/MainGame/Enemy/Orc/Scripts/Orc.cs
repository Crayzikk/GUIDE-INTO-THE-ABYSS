public class Orc : Enemy
{
    protected override int health { get; set; } = 350;
    protected override int damage { get; set; } = 30;
    protected override float attackRate { get; set; } = 2.5f;
}
