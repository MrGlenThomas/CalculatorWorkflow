# CalculatorWorkflow
A sample WCF Windows Workflow Foundation project

 
#INTRODUCTION
##OVERVIEW

Windows Workflow Foundation (WF) is a Microsoft technology that provides an API, an in-process workflow engine, and a rehostable designer to implement long-running processes as workflows within .NET applications.
The purpose of this tutorial is to provide an understanding of some basic Windows Workflow Foundation techniques for setting up new workflow services.
We will cover creating a Windows Communication Foundation (WCF) workflow service application that performs some simple tasks.

##BENEFITS OF USING WORKFLOW SERVICES
As applications become increasingly distributed, individual services become responsible for calling other services to offload some of the work. Implementing these calls as asynchronous operations introduces some complexity into the code. Error handling adds additional complexity in the form of handling exceptions and providing detailed tracking information. Some services are often long running and can take up valuable system resources while waiting for input. Because of these issues, distributed applications are often very complex and difficult to write and maintain. Workflows are a natural way to express the coordination of asynchronous work, especially calls to external services. Workflows are also effective at representing long-running business processes.

 
#PART 1 – CREATING A WCF WORKFLOW SERVICE

Open Visual Studio 2015 and select File -> New -> Project.
Under the Visual C# project templates, select Workflow and then choose the WCF Workflow Service Application project type.
In the name box enter “CalculatorWorkflowService” and click OK.
 
A basic workflow service called “Service1.xamlx” has been created and is displayed in the workflow designer for editing.
 
Right-click the service xamlx file and select Rename. Enter the name “MultiplyWorkflow.xamlx”. Right-click the xamlx file again and select “View Code”. Change the name attribute of the WorkflowService to “MultiplyWorkflow”.

##STARTING A WORKFLOW INSTANCE
The default workflow template consists of a Receive activity followed by a SendReply activity. These two activities are added to the workflow to provide a way to begin a new workflow instance. The Receive activity will generate a WCF SOAP operation using contract inference. 

###Contract Inference
When a workflow service is hosted using WorkflowServiceHost, the workflow definition is examined and a contract is generated based on the set of messaging activities found in the workflow. The end result of contract inference is a description of the service using the same data structures as WCF service and operation contracts. This information is then used to expose WSDL for the workflow service.
Right-click on the Receive activity and select Properties. In the properties inspector you will see that the CanCreateInstance option is checked. This will cause a new workflow instance to be created when the operation is invoked.
The Receive activity is paired with a SendReply activity to transmit a response object to the caller of the Receive method.
Click in the OperationName text box of the Receive activity in the designer window and change the OperationName to “Multiply”.
 
##CREATING WORKFLOW VARIABLES
The Multiply service method will accept two integer parameters and we will need to store the parameters being passed for the rest of the workflow to access them.
Click the Variables button at the bottom of the workflow designer to access the list of workflow variables and select the “Sequential Service” sequence activity to see all variables within that scope.
 
There are two variables added by default, which are not required and may be deleted. 
Click on the new line (“Create Variable”) and add a new variable with name “x” and type Int32. Then add another called “y”.
 
##ADDING INPUT PARAMETERS TO THE WORKFLOW
Right-click on the Receive activity and select Properties. In the properties inspector click the “…” button of the Content property. This will open the Content Definition window where we can add parameters to the operation.
Select the Parameters radio button and add two Int32 parameters called “num1” and “num2” and assign to the workflow variables x and y.
 
##ADDING A WORKFLOW ACTIVITY
We will now add a new activity to the workflow that will multiply the two variables. Firstly, add a new Int32 workflow variable to the sequence scope called “result”, this will hold the result of the multiplication.
Now open the Toolbox pane in Visual Studio with the workflow designer open and expand the Primitives group.
 
Select the assign activity and drag-drop the activity onto the workflow designer between the Receive and SendReply activities.
In the To box enter “result” to specify that the result workflow variable will be assigned to. In the C# expression box enter “x * y” so that the result variable will be assigned the multiplication product of the x and y variables.
 
##SEND THE REPONSE	
To send the result variable to the caller of the Multiply method, click the “…” button of the Content property of the SendReply activity to open the Content Definition window.
Select the Parameters radio button and add a parameter called result of type Int32, set the value to the result workflow variable and click OK.
 
Setting the Content property of the SendReply activity will change the return type of the service method generated through contract inference to the type specified and the value of the return object will be set automatically to what is specified in the value field. This could be a workflow variable, a C# expression or simply a constant value.

#PART 2 – HOSTING THE WORKFLOW SERVICE
##CONFIGURING THE SERVICE PROJECT
Right-click the CalculatorWorkflowService project and select Properties. Open the Web settings and under the Server section select “Local IIS”. You will see a prompt asking if you want to configure the virtual directory; click Yes.

###IIS Hosting
IIS features must be enabled in Windows for the Local IIS option to be available and Visual Studio must be run as an administrator in order to create the virtual directory in IIS.
.NET WCF Services HTTP Activation feature must also be enabled
 
Right-click the CalculatorWorkflowService project and select “Build” to build the workflow service application. This will add the CalculatorWorkflowService application to the default web site in IIS.
 
With the application selected, click the “Content View” button to see the files in the virtual directory. Right-click the MultiplyWorkflow.xamlx file and select “Browse” to load the service in the default web browser and confirm that all is set up correctly. You will see the default WCF service page
 

#PART 3 – TESTING THE WORKFLOW SERVICE
##CREATING A TEST PROJECT
Right-click the CalculatorWorkflowService solution and select Add -> New Project.
Select the Test section under Visual C# and choose the Unit Test Project template.
Enter “CalculatorWorkflowService.Tests” for the project name and click OK.
 
Rename the default test class UnitTest1 to “MultiplyTests” and open it. Rename the default test method TestMethod1 to “MultiplyTest”.

##ADDING A SERVICE REFERENCE TO THE WORKFLOW SERVICE
Right-click the CalculatorWorkflowService.Tests project and select Add -> Service Reference.
In the Address field enter the URL to the MultiplyWorkflow xamlx (http://localhost/CalculatorWorkflowService/MultiplyWorkflow.xamlx) and click Go. The operations from the workflow WCF service contract will be displayed. Change the namespace to “MultiplyWorkflowService” and click OK. A Service References folder containing the operation contract will be added to the test project.

##WRITING THE WORKFLOW TEST
Add the following code to the MultiplyTest method:

```C#
var num1 = 2;
var num2 = 5;
var expectedResult = 10;
int actualResult;
	
using (var multiplyClient = new MultiplyWorkflowService.ServiceClient())
{
    actualResult = multiplyClient.Multiply(num1, num2);
}

Assert.AreEqual(expectedResult, actualResult);
```

The test creates a WCF service client for the MultiplyWorkflow service and invokes the multiply method passing two integers as parameters and receiving an integer result.

Run the test and check that the assertion passes.

#PART 4 – ADDING A SERVICE METHOD AS AN ACTIVITY
##THE OBJECTIVE
In order to make the multiplication log reusable across workflows we will refactor it to a web service method that the workflow can make use of as an activity.

##CREATING THE SUPPORTING WEB SERVICE
In Visual Studio 2015 select File -> New -> Project.
Under the Visual C# project templates, select WCF and then choose the WCF Service Application project template.
In the name box enter “CalculatorWorkflowService.Web” and click OK.
 
A basic WCF service interface called “IService1” and implementation called “Service1.svc have been created.
Rename the IService1 interface to ICalculatorService and implementation class to CalculatorService.
Right-click the newly renamed CalculatorService.svc and select “View Markup”. Change the Service attribute of the ServiceHost element from "CalculatorWorkflowService.Web.Service1" to "CalculatorWorkflowService.Web.CalculatorService ".
Remove the CompositeType data contract and the default methods from interface and class.
Add the following code to the interface to create a multiply operation contract for the service:

```C#
[OperationContract]
int Multiply(int value1, int value2);
```

Now add the implementation of the method in the CalculatorService class:

```C#
public int Multiply(int value1, int value2)
{
    return value1 * value2;
}
```

Open the CalculatorWorkflowService.Web project settings and under the Web section change the server to Local IIS.
Now build the CalculatorWorkflowService.Web project to set up the new WCF service in IIS.

##ADDING A SERVICE REFERNCE TO THE WEB SERVICE
Now that the web service is running in IIS we can download the service contract to the workflow project so that we can use the service methods as activities.
Right-click the CalculatorWorkflowService (WCF workflow) project and select Add -> Service Reference.
In the Address field enter the URL to the CalculatorService.svc (http://localhost/CalculatorWorkflowService.Web/CalculatorService.svc) and click Go. The operations from the WCF service contract will be displayed. Change the namespace to “CalculatorServices” and click OK. A Service References folder containing the operation contract will be added to the test project.
A notification will be displayed to let you know that new activities have been generated.
 
##ADDING THE SERVICE METHOD ACTIVITY TO THE WORKFLOW
Right-click the CalculatorWorkflowService project and select Build to generate the new activity.
Open the MultiplyWorkflow xamlx in the workflow designer and delete the Assign activity that we added earlier.
Expand the toolbox and you will see a new section labelled “CalculatorWorkflowService.CalculatorServices.Activities”.
Drag-drop the Multiply activity to the workflow at the same position that the Assign activity used to be.
Open the activity properties in the property inspector and set the properties as below:
* MultiplyResult = result
* value1 = x
* value2 = y

Right-click the CalculatorWorkflowService project and select Build.

##TESTING THE WORKFLOW
Open the MultiplyTests class that we created earlier and run the MultiplyTest method to assert that the workflow still executes with the same logic as used previously.
