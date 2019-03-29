# StringReference
Use small reference handles instead of strings, to de-duplicate string memory in large numbers of classes (JSON values, config classes, etc).

One drawback is that once a string is cached, it remains in memory for the duration of the application.

void Example()
{
    StringReference r = "myString";

    print(string.Format("{0} == {1}? {2}", r.ToString(), "myString", r == "myString")); // True
    print(string.Format("{0} == {1}? {2}", r.ToString(), "myString2", r == "myString2")); // False
    print(string.Format("Num cached strings: {0}", StringReference.NumStringsStored()));

    print(string.Format("Num cached strings: {0}", StringReference.NumStringsStored())); // 1
    StringReference r2 = "myString2";
    print(string.Format("Num cached strings: {0}", StringReference.NumStringsStored())); // 2
    
    StringReference r3 = "myString2";
    StringReference r4 = "myString2";
    StringReference r5 = "myString2";
    StringReference r6 = "myString2";
    StringReference r7 = "myString2";
    StringReference r8 = "myString2";
    print(string.Format("Num cached strings: {0}", StringReference.NumStringsStored())); // 2
}