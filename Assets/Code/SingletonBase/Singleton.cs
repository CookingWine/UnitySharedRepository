/// <summary>
/// 单例基类。基于反射。线程安全。
/// </summary>
public abstract class Singleton<T> :ISingleton where T : Singleton<T>
{
    /// <summary>
    /// 初始化一个新的实例
    /// </summary>
    protected Singleton( ) { }

    private static T instance = null;

    private static readonly object sysLock = new object( );

    /// <summary>
    /// 获取一个实例
    /// <para></para>
    /// </summary>
    public static T Instance
    {
        get
        {
            lock( sysLock )
            {
                instance ??= SingletonCreator.CreateSingleton<T>( );
            }

            return instance;
        }
    }

    /// <summary>
    /// 单例初始化
    /// </summary>
    public virtual void OnSingletonInit( ) { }
}
