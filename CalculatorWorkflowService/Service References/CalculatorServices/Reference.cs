﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CalculatorWorkflowService.CalculatorServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CalculatorServices.ICalculatorService")]
    public interface ICalculatorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatorService/Multiply", ReplyAction="http://tempuri.org/ICalculatorService/MultiplyResponse")]
        CalculatorWorkflowService.CalculatorServices.MultiplyResponse Multiply(CalculatorWorkflowService.CalculatorServices.MultiplyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatorService/Multiply", ReplyAction="http://tempuri.org/ICalculatorService/MultiplyResponse")]
        System.Threading.Tasks.Task<CalculatorWorkflowService.CalculatorServices.MultiplyResponse> MultiplyAsync(CalculatorWorkflowService.CalculatorServices.MultiplyRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Multiply", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class MultiplyRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int value1;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int value2;
        
        public MultiplyRequest() {
        }
        
        public MultiplyRequest(int value1, int value2) {
            this.value1 = value1;
            this.value2 = value2;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MultiplyResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class MultiplyResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int MultiplyResult;
        
        public MultiplyResponse() {
        }
        
        public MultiplyResponse(int MultiplyResult) {
            this.MultiplyResult = MultiplyResult;
        }
    }
}
