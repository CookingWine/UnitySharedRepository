using UnityEngine;

/// <summary>
/// MonoBehaviour单例基类。线程安全。
/// </summary>
public class MonoSingleton<T> :MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;

    private static readonly object sysLock = new object( );

    ///<summary>
    /// 得到一个实例。Mono单例类。
    ///</summary>
    public static T Instance
    {
        get
        {
            lock( sysLock )
            {
                instance ??= SingletonCreator.CreateMonoSingleton<T>( );
            }
            return instance;
        }
    }

    /// <summary>
    /// MonoSingleton对象不会在加载新场景时自动销毁
    ///<para></para> 
    /// </summary>
    protected void DontDestroyOnLoad( )
    {
        DontDestroyOnLoad( this );
    }

    /// <summary>
    /// 调用此函数。MonoSingleton对象被销毁
    /// </summary>
    protected virtual void OnDestroy( )
    {
        instance = null;
    }
}
