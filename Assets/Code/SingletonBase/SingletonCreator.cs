using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 单例接口
/// </summary>
public interface ISingleton
{
    /// <summary>
    /// 单例对象初始化
    /// </summary>
    void OnSingletonInit( );
}

public static class SingletonCreator
{
    /// <summary>
    /// 创建一个单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateSingleton<T>( ) where T : class, ISingleton
    {
        // 获取私有构造函数
        var ctors = typeof( T ).GetConstructors( BindingFlags.Instance | BindingFlags.NonPublic );

        // 获取无参构造函数
        var ctor = Array.Find( ctors , a => a.GetParameters( ).Length == 0 );

        if( ctor == null )
        {
            throw new SingletonException( "Non-Public Constructor() not found! in " + typeof( T ) );
        }

        T value = ctor.Invoke( null ) as T;

        value.OnSingletonInit( );

        return value;
    }

    /// <summary>
    /// 创建一个Mono单例
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateMonoSingleton<T>( ) where T : MonoBehaviour
    {
        T value = MonoBehaviour.FindObjectOfType<T>( );

        if( MonoBehaviour.FindObjectsOfType<T>( ).Length > 1 )
        {
            throw new SingletonException( typeof( T ) + " object more than 1!" );
        }

        if( value == null )
        {
            string name = typeof( T ).Name;
            GameObject game = GameObject.Find( name );
            if( game == null )
            {
                game = new GameObject( name , typeof( T ) );
            }
            value = game.GetComponent<T>( );
        }

        return value;
    }
}