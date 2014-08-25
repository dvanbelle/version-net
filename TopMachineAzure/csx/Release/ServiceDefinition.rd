<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="TopMachineAzure" generation="1" functional="0" release="0" Id="6b13a55f-9985-4f37-9bc4-4fe6b1988337" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="TopMachineAzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="TopMachine.Services.Wcf.SQL:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/TopMachineAzure/TopMachineAzureGroup/LB:TopMachine.Services.Wcf.SQL:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="TopMachine.Services.Wcf.SQLInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/TopMachineAzure/TopMachineAzureGroup/MapTopMachine.Services.Wcf.SQLInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:TopMachine.Services.Wcf.SQL:Endpoint1">
          <toPorts>
            <inPortMoniker name="/TopMachineAzure/TopMachineAzureGroup/TopMachine.Services.Wcf.SQL/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapTopMachine.Services.Wcf.SQLInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/TopMachineAzure/TopMachineAzureGroup/TopMachine.Services.Wcf.SQLInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="TopMachine.Services.Wcf.SQL" generation="1" functional="0" release="0" software="D:\Ict7\Sample\TopMachine.Desktop.2012\TopMachineAzure\csx\Release\roles\TopMachine.Services.Wcf.SQL" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="768" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;TopMachine.Services.Wcf.SQL&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;TopMachine.Services.Wcf.SQL&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="TopMachineServiceWCF.svclog" defaultAmount="[1000,1000,1000]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/TopMachineAzure/TopMachineAzureGroup/TopMachine.Services.Wcf.SQLInstances" />
            <sCSPolicyUpdateDomainMoniker name="/TopMachineAzure/TopMachineAzureGroup/TopMachine.Services.Wcf.SQLUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/TopMachineAzure/TopMachineAzureGroup/TopMachine.Services.Wcf.SQLFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="TopMachine.Services.Wcf.SQLUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="TopMachine.Services.Wcf.SQLFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="TopMachine.Services.Wcf.SQLInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="44723e89-bca9-4c6b-8b4c-45c1fdbc2262" ref="Microsoft.RedDog.Contract\ServiceContract\TopMachineAzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="b1e5dd44-3c74-42e2-a417-2890379ed0b1" ref="Microsoft.RedDog.Contract\Interface\TopMachine.Services.Wcf.SQL:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/TopMachineAzure/TopMachineAzureGroup/TopMachine.Services.Wcf.SQL:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>