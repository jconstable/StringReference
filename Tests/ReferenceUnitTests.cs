using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class StringReferenceUnitTests
    {
        [Test]
        public void StringReferenceUnitTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            Debug.Assert(!(new StringReference() == new StringReference("test")));
            Debug.Assert(!(new StringReference() == "test"));
            Debug.Assert(new StringReference() == new StringReference());

            StringReference p = "test";

            Debug.Log(p.ToString());

            string a = "test";

            Debug.Assert((a == p));
            Debug.Assert((p == a));
            Debug.Assert(!(a != p));
            Debug.Assert(!(p != a));

            StringReference other = "other";
            p = other;

            Debug.Log(p.ToString());
            Debug.Assert(!(a == p));
            Debug.Assert(!(p == a));
            Debug.Assert((a != p));
            Debug.Assert((p != a));

            Debug.Log("s is null");
            a = null;
            Debug.Assert(!(a == p));
            Debug.Assert(!(p == a));
            Debug.Assert((a != p));
            Debug.Assert((p != a));

            Debug.Log("p is null");
            p = null;
            a = "test";
            Debug.Assert(!(a == p));
            Debug.Assert(!(p == a));
            Debug.Assert((a != p));
            Debug.Assert((p != a));

            Debug.Log("both are null");
            p = null;
            a = null;
            Debug.Assert((a == p));
            Debug.Assert((p == a));
            Debug.Assert(!(a != p));
            Debug.Assert(!(p != a));

            StringReference duplicate1 = "test";
            StringReference duplicate2 = "test";
            StringReference duplicate3 = "test";
            StringReference duplicate4 = "test";
            StringReference duplicate5 = "test";
            StringReference duplicate6 = "test";
            StringReference duplicate7 = "test";

            Debug.Assert(StringReference.NumStringsStored() == 2);
        }
    }
}
