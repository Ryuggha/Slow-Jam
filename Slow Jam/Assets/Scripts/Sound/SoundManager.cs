using UnityEngine;
using FMODUnity;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public static void OneShot (EventReference r)
    {
        RuntimeManager.PlayOneShot(r);
    }

    public static void OneShot(EventReference r, SoundParamater[] parameters)
    {
        var instance = RuntimeManager.CreateInstance(r);
        foreach (var param in parameters)
        {
            instance.setParameterByName(param.parameterName, param.isInt ? param.valueInt : param.valueFloat);
        }
        instance.start();
        instance.release();
    }
}

public class SoundParamater
{
    public string parameterName { get; private set; }
    public int valueInt { get; private set; }
    public float valueFloat { get; private set; }
    public bool isInt { get; private set; }

    public SoundParamater(string parameterName, int valueInt)
    {
        this.parameterName = parameterName;
        this.valueInt = valueInt;
        isInt = true;
    }

    public SoundParamater(string parameterName, float valueFloat)
    {
        this.parameterName = parameterName;
        this.valueFloat = valueFloat;
        isInt = false;
    }
}
