﻿using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;

namespace AppInsightsAction.Tests
{
    [TestClass]
    public class ActionMetricTests
    {
        [TestMethod]
        public void ActionMetric_Measurement_Test()
        {
            AiSecureConfig aiSecureConfig =
                AppInsightsShared.Tests.Configs.GetAiSecureConfig(false, false, false, false, false, false);

            string secureConfig = SerializationHelper.SerializeObject<AiSecureConfig>(aiSecureConfig);

            XrmFakedContext context = new XrmFakedContext();

            XrmFakedPluginExecutionContext xrmFakedPluginExecution = new XrmFakedPluginExecutionContext();
            Guid userId = Guid.Parse("9e7ec57b-3a08-4a41-a4d4-354d66f19b65");
            xrmFakedPluginExecution.InitiatingUserId = userId;
            xrmFakedPluginExecution.UserId = userId;
            xrmFakedPluginExecution.CorrelationId = Guid.Parse("15cc775b-9ebc-48d1-93a6-b0ce9c920b66");
            xrmFakedPluginExecution.MessageName = "update";
            xrmFakedPluginExecution.Mode = 1;
            xrmFakedPluginExecution.Depth = 1;
            xrmFakedPluginExecution.OrganizationName = "test.crm.dynamics.com";
            xrmFakedPluginExecution.Stage = 40;

            xrmFakedPluginExecution.InputParameters = GetInputParameters();

            xrmFakedPluginExecution.OutputParameters = new ParameterCollection();

            context.ExecutePluginWithConfigurations<LogMetric>(xrmFakedPluginExecution, null, secureConfig);

            Assert.IsTrue((bool)xrmFakedPluginExecution.OutputParameters["logsuccess"]);
        }

        [TestMethod]
        public void ActionMetric_Measurement_Missing_Name_Test()
        {
            AiSecureConfig aiSecureConfig =
                AppInsightsShared.Tests.Configs.GetAiSecureConfig(false, false, false, false, false, false);

            string secureConfig = SerializationHelper.SerializeObject<AiSecureConfig>(aiSecureConfig);

            XrmFakedContext context = new XrmFakedContext();

            XrmFakedPluginExecutionContext xrmFakedPluginExecution = new XrmFakedPluginExecutionContext();

            xrmFakedPluginExecution.InputParameters = GetInputParameters();
            xrmFakedPluginExecution.InputParameters.Remove("name");

            xrmFakedPluginExecution.OutputParameters = new ParameterCollection();

            context.ExecutePluginWithConfigurations<LogMetric>(xrmFakedPluginExecution, null, secureConfig);

            Assert.IsFalse((bool)xrmFakedPluginExecution.OutputParameters["logsuccess"]);
            Assert.IsTrue(xrmFakedPluginExecution.OutputParameters["errormessage"].ToString() == "Name must be populated");
        }

        [TestMethod]
        public void ActionMetric_Measurement_Missing_Kind_Test()
        {
            AiSecureConfig aiSecureConfig =
                AppInsightsShared.Tests.Configs.GetAiSecureConfig(false, false, false, false, false, false);

            string secureConfig = SerializationHelper.SerializeObject<AiSecureConfig>(aiSecureConfig);

            XrmFakedContext context = new XrmFakedContext();

            XrmFakedPluginExecutionContext xrmFakedPluginExecution = new XrmFakedPluginExecutionContext();

            xrmFakedPluginExecution.InputParameters = GetInputParameters();
            xrmFakedPluginExecution.InputParameters.Remove("kind");

            xrmFakedPluginExecution.OutputParameters = new ParameterCollection();

            context.ExecutePluginWithConfigurations<LogMetric>(xrmFakedPluginExecution, null, secureConfig);

            Assert.IsFalse((bool)xrmFakedPluginExecution.OutputParameters["logsuccess"]);
            Assert.IsTrue(xrmFakedPluginExecution.OutputParameters["errormessage"].ToString() == "Kind must be populated");
        }

        [TestMethod]
        public void ActionMetric_Measurement_Missing_Value_Test()
        {
            AiSecureConfig aiSecureConfig =
                AppInsightsShared.Tests.Configs.GetAiSecureConfig(false, false, false, false, false, false);

            string secureConfig = SerializationHelper.SerializeObject<AiSecureConfig>(aiSecureConfig);

            XrmFakedContext context = new XrmFakedContext();

            XrmFakedPluginExecutionContext xrmFakedPluginExecution = new XrmFakedPluginExecutionContext();

            xrmFakedPluginExecution.InputParameters = GetInputParameters();
            xrmFakedPluginExecution.InputParameters.Remove("value");

            xrmFakedPluginExecution.OutputParameters = new ParameterCollection();

            context.ExecutePluginWithConfigurations<LogMetric>(xrmFakedPluginExecution, null, secureConfig);

            Assert.IsFalse((bool)xrmFakedPluginExecution.OutputParameters["logsuccess"]);
            Assert.IsTrue(xrmFakedPluginExecution.OutputParameters["errormessage"].ToString() == "Value must be populated");
        }

        [TestMethod]
        public void ActionMetric_Measurement_Invalid_Kind_Test()
        {
            AiSecureConfig aiSecureConfig =
                AppInsightsShared.Tests.Configs.GetAiSecureConfig(false, false, false, false, false, false);

            string secureConfig = SerializationHelper.SerializeObject<AiSecureConfig>(aiSecureConfig);

            XrmFakedContext context = new XrmFakedContext();

            XrmFakedPluginExecutionContext xrmFakedPluginExecution = new XrmFakedPluginExecutionContext();

            xrmFakedPluginExecution.InputParameters = GetInputParameters();
            xrmFakedPluginExecution.InputParameters["kind"] = 5;

            xrmFakedPluginExecution.OutputParameters = new ParameterCollection();

            context.ExecutePluginWithConfigurations<LogMetric>(xrmFakedPluginExecution, null, secureConfig);

            Assert.IsFalse((bool)xrmFakedPluginExecution.OutputParameters["logsuccess"]);
            Assert.IsTrue(xrmFakedPluginExecution.OutputParameters["errormessage"].ToString() == "Invalid DataPointType, should be 0 (Measurement) or 1 (Aggregation)");
        }

        [TestMethod]
        public void ActionMetric_Aggregate_Test()
        {
            AiSecureConfig aiSecureConfig =
                AppInsightsShared.Tests.Configs.GetAiSecureConfig(false, false, false, false, false, false);

            string secureConfig = SerializationHelper.SerializeObject<AiSecureConfig>(aiSecureConfig);

            XrmFakedContext context = new XrmFakedContext();

            XrmFakedPluginExecutionContext xrmFakedPluginExecution = new XrmFakedPluginExecutionContext();
            Guid userId = Guid.Parse("9e7ec57b-3a08-4a41-a4d4-354d66f19b65");
            xrmFakedPluginExecution.InitiatingUserId = userId;
            xrmFakedPluginExecution.UserId = userId;
            xrmFakedPluginExecution.CorrelationId = Guid.Parse("15cc775b-9ebc-48d1-93a6-b0ce9c920b66");
            xrmFakedPluginExecution.MessageName = "update";
            xrmFakedPluginExecution.Mode = 1;
            xrmFakedPluginExecution.Depth = 1;
            xrmFakedPluginExecution.OrganizationName = "test.crm.dynamics.com";
            xrmFakedPluginExecution.Stage = 40;

            xrmFakedPluginExecution.InputParameters = GetInputParameters();

            xrmFakedPluginExecution.OutputParameters = new ParameterCollection {
                new System.Collections.Generic.KeyValuePair<string, object>("name", "Hello from MetricTest - 1"),
                new System.Collections.Generic.KeyValuePair<string, object>("kind", 1),
                new System.Collections.Generic.KeyValuePair<string, object>("value", 34),
                new System.Collections.Generic.KeyValuePair<string, object>("count", 1),
                new System.Collections.Generic.KeyValuePair<string, object>("min", 34),
                new System.Collections.Generic.KeyValuePair<string, object>("max", 34)
            };

            context.ExecutePluginWithConfigurations<LogMetric>(xrmFakedPluginExecution, null, secureConfig);

            Assert.IsTrue((bool)xrmFakedPluginExecution.OutputParameters["logsuccess"]);
        }

        private static ParameterCollection GetInputParameters()
        {
            return new ParameterCollection {
                new System.Collections.Generic.KeyValuePair<string, object>("name", "Hello from MetricTest - 1"),
                new System.Collections.Generic.KeyValuePair<string, object>("kind", 0),
                new System.Collections.Generic.KeyValuePair<string, object>("value", 34)
            };
        }
    }
}