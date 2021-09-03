// Вставьте сюда финальное содержимое файла Specifier.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.Attribute;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        readonly Type type = typeof(T);
        public string GetApiDescription() => type.GetCustomAttributes(true)
                .OfType<ApiDescriptionAttribute>()
                .FirstOrDefault()?.Description;

        public string[] GetApiMethodNames() => type.GetMethods()
                .Where(methodInfo => methodInfo.GetCustomAttributes<ApiMethodAttribute>().Any())
                .Select(methodInfo => methodInfo.Name)
                .ToArray();

        public string GetApiMethodDescription(string methodName) => type.GetMethods()
                .Where(methodInfo => methodInfo.Name == methodName)
                .Select(methodInfo => GetCustomAttribute<ApiDescriptionAttribute>(methodInfo)?.Description)
                .FirstOrDefault();

        public string[] GetApiMethodParamNames(string methodName) => type.GetMethods()
                .FirstOrDefault(methodInfo => methodInfo.Name == methodName)?
                .GetParameters()
                .Select(parameterInfo => parameterInfo.Name)
                .ToArray();

        public string GetApiMethodParamDescription(string methodName, string paramName) =>
            GetMethodParam(methodName, paramName) == null ? null :
                GetCustomAttribute<ApiDescriptionAttribute>(GetMethodParam(methodName, paramName))?.Description;

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var result = new ApiParamDescription();
            ValidAndRequired(GetMethodParam(methodName, paramName), result);
            result.ParamDescription = new CommonDescription(paramName,
                GetApiMethodParamDescription(methodName, paramName));
            return result;
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var methodInfo = type.GetMethod(methodName);
            var paramDescription = new ApiParamDescription { ParamDescription = new CommonDescription() };
            var validationAttribute = methodInfo?.ReturnParameter?
                .GetCustomAttributes(true).OfType<ApiIntValidationAttribute>().FirstOrDefault();
            var requiredAttribute = methodInfo?.ReturnParameter?
                .GetCustomAttributes(true).OfType<ApiRequiredAttribute>().FirstOrDefault();
            if (methodInfo != null &&
                !methodInfo.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any()) return null;
            var result = new ApiMethodDescription
            {
                MethodDescription = new CommonDescription(methodName, GetApiMethodDescription(methodName)),
                ParamDescriptions = GetApiMethodParamNames(methodName).Select(paramName =>
                    GetApiMethodParamFullDescription(methodName, paramName)).ToArray()
            };
            if (requiredAttribute != null)
            {
                paramDescription.Required = requiredAttribute.Required;
                result.ReturnDescription = paramDescription;
            }
            if (validationAttribute == null) return result;
            paramDescription.MinValue = validationAttribute.MinValue;
            paramDescription.MaxValue = validationAttribute.MaxValue;
            return result;
        }

        private static void ValidAndRequired(ParameterInfo param, ApiParamDescription result)
        {
            var validationAttribute = GetCustomAttribute<ApiIntValidationAttribute>(param);
            var requiredAttribute = GetCustomAttribute<ApiRequiredAttribute>(param);
            if (requiredAttribute != null)
                result.Required = requiredAttribute.Required;
            if (validationAttribute == null) return;
            result.MaxValue = validationAttribute.MaxValue;
            result.MinValue = validationAttribute.MinValue;
        }

        private static TAttr GetCustomAttribute<TAttr>(ParameterInfo param) where TAttr : class =>
            param == null ? null : Attribute.GetCustomAttribute(param, typeof(TAttr)) as TAttr;

        private static TAttr GetCustomAttribute<TAttr>(MethodInfo param) where TAttr : class =>
            param == null ? null : Attribute.GetCustomAttribute(param, typeof(TAttr)) as TAttr;

        private static ParameterInfo GetMethodParam(string methodName, string paramName) => typeof(T)
                .GetMethods()
                .FirstOrDefault(methodInfo => methodInfo.Name == methodName)?
                .GetParameters()
                .FirstOrDefault(parameterInfo => parameterInfo.Name == paramName);
    }
}