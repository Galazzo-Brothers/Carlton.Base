namespace Carlton.Base.Infrastructure.Test.UnitTesting.XUnit;

public static class XUnitParserSampleSource
{
    public const string Content_1_NoTraits =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"" total=""335"" passed=""333"" failed=""2"" skipped=""0"" time=""7.446"" errors=""0"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_3"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test3"" time=""0.0056237"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_4"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test4"" time=""0.0340880"" result=""Pass"">
        <traits />
      </test>
    </collection>
    <collection total = ""2"" passed=""1"" failed=""1"" skipped=""0"" name=""Test Component 2"" time=""0.407"">
      <test name = ""Assembly_1_Component_2_Test_1"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test1"" time=""0.1442009"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_2_Test_2"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test2"" time=""0.0127423"" result=""Fail"">
        <failure>
          <message>Some Error Message</message>
          <stack-trace>Some Stack Trace</stack-trace>
        </failure>
        <traits />
      </test>
    </collection>
    <collection total = ""1"" passed=""1"" failed=""0"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_1_Component_3_Test_1"" type=""Assembly1.Test.Component3"" method=""Assembly1TestComponent3Test1"" time=""0.0405402"" result=""Pass"">
        <traits />
      </test>
    </collection>
  </assembly>
</assemblies>";

    public const string Content_1_WithTraits =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"" total=""335"" passed=""333"" failed=""2"" skipped=""0"" time=""7.446"" errors=""0"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_3"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test3"" time=""0.0056237"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 3"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_4"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test4"" time=""0.0340880"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 4"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
    </collection>
    <collection total = ""2"" passed=""1"" failed=""1"" skipped=""0"" name=""Test Component 2"" time=""0.407"">
      <test name = ""Assembly_1_Component_2_Test_1"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test1"" time=""0.1442009"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 2 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_2_Test_2"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test2"" time=""0.0127423"" result=""Fail"">
        <failure>
          <message>Some Error Message</message>
          <stack-trace>Some Stack Trace</stack-trace>
        </failure>
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 2 Test 2"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
    </collection>
    <collection total = ""1"" passed=""1"" failed=""0"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_1_Component_3_Test_1"" type=""Assembly1.Test.Component3"" method=""Assembly1TestComponent3Test1"" time=""0.0405402"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 3 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
    </collection>
  </assembly>
</assemblies>";

    public const string Content_2_NoTraits =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Fail"">
        <traits />
      </test>
    </collection>
  </assembly>
  <assembly name = ""C:\Test\Assembly2.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""3"" passed=""1"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_2_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.0530211"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_2_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.0433452"" result=""Fail"">
        <traits />
      </test>
    </collection>
  </assembly>
 <assembly name = ""C:\Test\Assembly3.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""2"" passed=""0"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_3_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.3242322"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_3_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.3423426"" result=""Fail"">
        <traits />
      </test>
    </collection>
  </assembly>
</assemblies>";

    public const string Content_2_WithTraits =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Fail"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
    </collection>
  </assembly>
  <assembly name = ""C:\Test\Assembly2.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""3"" passed=""1"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_2_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.0530211"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 2 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_2"" />
        </traits>
      </test>
      <test name = ""Assembly_2_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.0433452"" result=""Fail"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 2 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_2"" />
        </traits>
      </test>
    </collection>
  </assembly>
 <assembly name = ""C:\Test\Assembly3.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""2"" passed=""0"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_3_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.3242322"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 3 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_3"" />
        </traits>
      </test>
      <test name = ""Assembly_3_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.3423426"" result=""Fail"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 3 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_3"" />
        </traits>
      </test>
    </collection>
  </assembly>
</assemblies>";

    public const string Content_3_NoTraits =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_3"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test3"" time=""0.0056237"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_1_Test_4"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test4"" time=""0.0340880"" result=""Pass"">
        <traits />
      </test>
    </collection>
    <collection total = ""2"" passed=""1"" failed=""1"" skipped=""0"" name=""Test Component 2"" time=""0.407"">
      <test name = ""Assembly_1_Component_2_Test_1"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test1"" time=""0.1442009"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_1_Component_2_Test_2"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test2"" time=""0.0127423"" result=""Fail"">
        <failure>
          <message>Some Error Message</message>
          <stack-trace>Some Stack Trace</stack-trace>
        </failure>
        <traits />
      </test>
    </collection>
  </assembly>
  <assembly name = ""C:\Test\Assembly2.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""3"" passed=""1"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_2_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.0530211"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_2_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.0433452"" result=""Fail"">
        <traits />
      </test>
    </collection>
    <collection total = ""1"" passed=""1"" failed=""0"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_2_Component_2_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent2Test1"" time=""0.1110342"" result=""Pass"">
        <traits />
      </test>
    </collection>
  </assembly>
 <assembly name = ""C:\Test\Assembly3.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""2"" passed=""0"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_3_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.3242322"" result=""Pass"">
        <traits />
      </test>
      <test name = ""Assembly_3_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.3423426"" result=""Fail"">
        <traits />
      </test>
    </collection>
  </assembly>
</assemblies>";

    public const string Content_3_WithTraits =
        @"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_2"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_3"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test3"" time=""0.0056237"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 3"" />
            <trait name=""Category"" value=""Category_3"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_4"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test4"" time=""0.0340880"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 4"" />
            <trait name=""Category"" value=""Category_4"" />
        </traits>
      </test>
    </collection>
    <collection total = ""2"" passed=""1"" failed=""1"" skipped=""0"" name=""Test Component 2"" time=""0.407"">
      <test name = ""Assembly_1_Component_2_Test_1"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test1"" time=""0.1442009"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 2 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_2_Test_2"" type=""Assembly1.Test.Component2"" method=""Assembly1TestComponent2Test2"" time=""0.0127423"" result=""Fail"">
        <failure>
          <message>Some Error Message</message>
          <stack-trace>Some Stack Trace</stack-trace>
        </failure>
         <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 2 Test 2"" />
            <trait name=""Category"" value=""Category_2"" />
        </traits>
      </test>
    </collection>
  </assembly>
  <assembly name = ""C:\Test\Assembly2.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""3"" passed=""1"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_2_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.0530211"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 2 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_3"" />
        </traits>
      </test>
      <test name = ""Assembly_2_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.0433452"" result=""Fail"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 2 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_4"" />
        </traits>
      </test>
    </collection>
    <collection total = ""1"" passed=""1"" failed=""0"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_2_Component_2_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent2Test1"" time=""0.1110342"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 2 Component 2 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
    </collection>
  </assembly>
 <assembly name = ""C:\Test\Assembly3.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"">
    <collection total = ""2"" passed=""0"" failed=""2"" skipped=""0"" name=""Test Component 3"" time=""0.309"">
      <test name = ""Assembly_3_Component_1_Test_1"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test1"" time=""0.3242322"" result=""Pass"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 3 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_2"" />
        </traits>
      </test>
      <test name = ""Assembly_3_Component_1_Test_2"" type=""Assembly2.Test.Component2"" method=""Assembly2TestComponent1Test2"" time=""0.3423426"" result=""Fail"">
         <traits>
            <trait name=""DisplayName"" value=""Assembly 3 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_3"" />
        </traits>
      </test>
    </collection>
  </assembly>
</assemblies>";

    public const string Content_4_With_Some_Traits_And_NoTraits =
@"<assemblies timestamp=""05/02/2023 22:57:51"">
  <assembly name = ""C:\Test\Assembly1.Test.dll"" run-date=""2023-05-02"" run-time=""22:57:51"" total=""335"" passed=""333"" failed=""2"" skipped=""0"" time=""7.446"" errors=""0"">
    <errors />
    <collection total = ""4"" passed=""4"" failed=""0"" skipped=""0"" name=""Test Component 1"" time=""0.071"">
      <test name = ""Assembly_1_Component_1_Test_1"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test1"" time=""0.0162284"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 1"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_2"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test2"" time=""0.0151508"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 2"" />
            <trait name=""Category"" value=""Category_1"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_3"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test3"" time=""0.0056237"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 3"" />
        </traits>
      </test>
      <test name = ""Assembly_1_Component_1_Test_4"" type=""Assembly1.Test.Component1"" method=""Assembly1TestComponent1Test4"" time=""0.0340880"" result=""Pass"">
        <traits>
            <trait name=""DisplayName"" value=""Assembly 1 Component 1 Test 4"" />
        </traits>
      </test>
    </collection>
  </assembly>
</assemblies>";
}