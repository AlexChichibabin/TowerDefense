namespace TowerDefense
{
    public enum Sound
    {
        Arrow = 0,
        ArrowHit = 1,
        EnemyDie = 2,
        EnemyWin = 3,
        PlayerLose = 4,
        PlayerWin = 5,
        BGM1 = 6,
        BGM2 = 7,
    }
    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}