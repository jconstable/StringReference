using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StringReference
{
    // Storage for cached string instances
    #region StringReferenceCache
    private class StringReferenceCache
    {
        private static readonly int InitialCapacity = 64;

        public static uint s_counter = 1;
        private static List<string> s_stringTable = new List<string>() { string.Empty };
        private static Dictionary<string, uint> s_stringLookup = new Dictionary<string, uint>() { { string.Empty, 0 } };

        public static int NumStringsStored()
        {
            return (int)(s_counter - 1);
        }

        public static uint AddString(string s)
        {
            uint index = s_counter++;
            s_stringTable.Add(s);
            s_stringLookup.Add(s, index);
            return index;
        }

        public static uint GetIndex(string s)
        {
            if (s == null)
                return 0;

            uint index;
            if (!s_stringLookup.TryGetValue(s, out index))
            {
                index = AddString(s);
            }
            return index;
        }

        public static string GetString(uint index)
        {
            string s = null;
            if (index > 0)
            {
                s = s_stringTable[(int)index];
            }
            return s;
        }
    }
    #endregion

    // Utility method for counting the number of unique strings in the cache
    public static int NumStringsStored()
    {
        return (int)(StringReferenceCache.s_counter - 1);
    }
    
    #region OperatorOverrides
    // Allow for code such as Debug.Log(myStringReference) instead of Debug.Log(myStringReference.ToString())
    public static implicit operator string(StringReference p)
    {
        return StringReferenceCache.GetString(p._pointerIndex);
    }
    
    public static bool operator ==(StringReference lhs, StringReference rhs)
    {
        if (lhs._pointerIndex == 0)
        {
            if (rhs._pointerIndex == 0)
                return true;

            return false;
        }
        if (rhs._pointerIndex == 0)
            return false;

        return lhs._pointerIndex == rhs._pointerIndex;
    }

    public static bool operator ==(StringReference lhs, string rhs)
    {
        // Check for null on left side.
        if (lhs._pointerIndex == 0)
        {
            if (rhs == null)
            {
                // null == null = true.
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return StringReferenceCache.GetString(lhs._pointerIndex).Equals(rhs);
    }

    public static bool operator ==(string lhs, StringReference rhs)
    {
        if (lhs == null)
        {
            if (rhs._pointerIndex == 0)
            {
                return true;
            }

            return false;
        }
        if (rhs._pointerIndex == 0)
        {
            return false;
        }
        return lhs.Equals(StringReferenceCache.GetString(rhs._pointerIndex));
    }

    public static bool operator !=(StringReference lhs, StringReference rhs)
    {
        if (lhs._pointerIndex == 0)
        {
            if (rhs._pointerIndex == 0)
                return false;

            return true;
        }
        if (rhs._pointerIndex == 0)
            return true;

        return !(lhs._pointerIndex == rhs._pointerIndex);
    }

    public static bool operator !=(StringReference lhs, string rhs)
    {
        if (lhs._pointerIndex == 0)
        {
            if (rhs == null)
                return false;

            return true;
        }
        if (rhs == null)
            return true;

        // Equals handles case of null on right side.
        return !StringReferenceCache.GetString(lhs._pointerIndex).Equals(rhs);
    }
    public static bool operator !=(string lhs, StringReference rhs)
    {
        if (lhs == null)
        {
            if (rhs._pointerIndex == 0)
            {
                return false;
            }

            return true;
        }
        if (rhs._pointerIndex == 0)
        {
            return true;
        }
        return !lhs.Equals(StringReferenceCache.GetString(rhs._pointerIndex));
    }

    public static implicit operator StringReference(string t)
    {
        StringReference p = new StringReference();
        p._pointerIndex = StringReferenceCache.GetIndex(t);
        return p;
    }
    #endregion

    private uint _pointerIndex;

    public StringReference(string s)
    {
        _pointerIndex = StringReferenceCache.GetIndex(s);
    }

    public string ToString()
    {
        string s = null;
        if(_pointerIndex > 0)
        {
            s = StringReferenceCache.GetString(_pointerIndex);
        }
        return s;
    }
}
