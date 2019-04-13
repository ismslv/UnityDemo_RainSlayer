using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Do {

    public static void PerformAfter(this MonoBehaviour behaviour, System.Action action, float time)
    {
        Core.a.StartCoroutine(performAfter(action, time));
    }

    public static void PerformAfter(this MonoBehaviour behaviour, System.Action action, float time, out IEnumerator coroutine)
    {
        coroutine = performAfter(action, time);
        behaviour.StartCoroutine(coroutine);
    }

    static IEnumerator performAfter(System.Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    public static void PerformAfterUnscaled(this MonoBehaviour behaviour, System.Action action, float time)
    {
        Core.a.StartCoroutine(performAfterUnscaled(action, time));
    }

    static IEnumerator performAfterUnscaled(System.Action action, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        action();
    }

    public static void RotateTo(this MonoBehaviour behaviour, Transform transform, Quaternion to, float speed)
    {
        behaviour.StartCoroutine(rotateFromTo(transform, transform.rotation, to, speed));
    }

    static IEnumerator rotateFromTo(Transform objectToMove, Quaternion a, Quaternion b, float speed)
    {
        float step = speed * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.rotation = Quaternion.Slerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.rotation = b;
    }

    public static void RotateLocalTo(this MonoBehaviour behaviour, Transform transform, Quaternion to, float speed)
    {
        behaviour.StartCoroutine(rotateLocalFromTo(transform, transform.localRotation, to, speed));
    }

    static IEnumerator rotateLocalFromTo(Transform objectToMove, Quaternion a, Quaternion b, float speed)
    {
        float step = speed * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.localRotation = Quaternion.Slerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.localRotation = b;
    }

    public static void MoveTo(this MonoBehaviour behaviour, Transform transform, Vector3 to, float speed, System.Action after)
    {
        behaviour.StartCoroutine(MoveFromTo(transform, transform.position, to, speed, after));
    }

    static IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed, System.Action after)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        if (after != null)
            after();
    }
}

public static class ExtensionMethods
{
    public static string AsDoubleDigitString(this int value)
    {
        return value < 10 ? "0" + value.ToString() : value.ToString();
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, Random.Range(i, list.Count));
    }

    public static void Shuffle<T>(this T[] list)
    {
        List<T> _list = new List<T>(list);
        _list.Shuffle();
        list = _list.ToArray();
    }

    public static void Swap<T>(this List<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static bool Approx(this float value, float goal, float distance = 0f)
    {
        if (distance == 0f)
        {
            return Mathf.Approximately(value, goal);
        } else
        {
            return Mathf.Abs(value - goal) <= distance;
        }
    }

    public static void Increment<T>(this Dictionary<T, int> dict, T what, int by)
    {
        if (dict.ContainsKey(what))
        {
            dict[what] += by;
        } else
        {
            dict[what] = by;
        }
    }

    public static void Increment<T>(this Dictionary<T, int> dict, T what)
    {
        dict.Increment(what, 1);
    }

    public static Color ParseColor(this string hexstring)
    {
        if (hexstring.StartsWith("#"))
        {
            hexstring = hexstring.Substring(1);
        }

        if (hexstring.StartsWith("0x"))
        {
            hexstring = hexstring.Substring(2);
        }

        byte r = byte.Parse(hexstring.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hexstring.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hexstring.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, byte.Parse("FF", System.Globalization.NumberStyles.HexNumber));
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360f || angle > 360f)
        {
            if (angle < -360f)
            {
                angle += 360f;
            }
            else if (angle > 360f)
            {
                angle -= 360f;
            }
        }

        return Mathf.Clamp(angle, min, max);
    }

    public static void IncrementArrayIndex(ref int index, int incrementBy, int arraylength)
    {
        index += incrementBy;
        if (incrementBy > 0)
        {
            if (index > arraylength - 1)
                index -= arraylength;
        }
        else
        {
            if (index < 0)
                index += arraylength;
        }
        index = Mathf.Clamp(index, 0, arraylength - 1);
    }

    public static Vector3 PointOnCircle(float angle, float radius)
    {
        return new Vector3(
            radius * Mathf.Cos(angle * Mathf.Deg2Rad),
            0,
            radius * Mathf.Sin(angle * Mathf.Deg2Rad)
        );
    }
}

public class Randomizer
{
    public static List<T> GetRandomItems<T>(List<T> list, int q)
    {
        list.Shuffle();
        List<T> newList = new List<T>();
        for (int i = 0; i < q; i++)
        {
            newList.Add(list[i]);
        }
        return newList;
    }

    public static List<T> GetRandomItems<T>(T[] array, int q)
    {
        return GetRandomItems(new List<T>(array), q);
    }

    public static List<T> GetRandomItems<T>(int q)
    {
        System.Type type = typeof(T);
        if (type.IsEnum)
        {
            List<T> list = new List<T>();
            foreach (string item in GetRandomItems(new List<string>((string[])System.Enum.GetNames(type)), q))
            {
                list.Add((T)System.Enum.Parse(type, item));
            };
            return list;
        }
        return null;
    }

    public static bool RandomBool()
    {
        return Random.Range(0, 2) == 1;
    }

    public static int GetStringRand(string rand)
    {
        return int.Parse(rand[Random.Range(0, rand.Length)].ToString());
    }

    public static List<int> GetListStringRand(string rand, int q)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < q; i++)
            list.Add(GetStringRand(rand));
        return list;
    }

    public static List<int> GetRandomItems(int total, int q)
    {
        int[] list = new int[total];
        for (int i = 0; i < total; i++)
            list[i] = i;
        return GetRandomItems(new List<int>(list), q);
    }
 }