// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// instance method

using System;
using Xunit;
namespace Test_precise2_simple_cs
{
internal class measure
{
    public static int a = 0xCC;
}
internal class test
{
    static test()
    {
        if (measure.a != 0xCC)
        {
            Console.WriteLine("in .cctor(), measure.a is {0}", measure.a);
            Console.WriteLine("FAILED");
            throw new Exception();
        }
        Console.WriteLine("in .cctor(), measure.a is {0}", measure.a);
        measure.a = 8;
        if (measure.a != 8)
        {
            Console.WriteLine("in .cctor() after measure.a=8, measure.a is {0}", measure.a);
            Console.WriteLine("FAILED");
            throw new Exception();
        }
        Console.WriteLine("in .cctor() after measure.a=8, measure.a is {0}", measure.a);
    }
}

public class Driver
{
    [Fact]
    [OuterLoop]
    public static int TestEntryPoint()
    {
        try
        {
            Console.WriteLine("Testing .cctor() invocation by calling instance method");
            Console.WriteLine();
            Console.WriteLine("Before calling instance method");
            // .cctor should not run yet
            if (measure.a != 0xCC)
            {
                Console.WriteLine("in Main(), measure.a is {0}", measure.a);
                Console.WriteLine("FAILED");
                return 1;
            }
            // the next line should trigger .cctor because .ctor is an instance method
            test t = new test();
            Console.WriteLine("After calling instance method");
            if (measure.a != 8)
            {
                Console.WriteLine("in Main() after new test(), measure.a is {0}", measure.a);
                Console.WriteLine("FAILED");
                return -1;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return -1;
        }
        Console.WriteLine();
        Console.WriteLine("PASSED");
        return 100;
    }
}
}
