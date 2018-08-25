using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <inheritdoc />
/// <summary>
/// Creates a button in the inspector when used on a bool field.
/// </summary>
public class ButtonAttribute : PropertyAttribute
{
                
        private readonly Delegate m_buttonCallback;
        private readonly bool m_validationExists = false;
        private readonly Delegate m_validationDelegate;

        /// <summary>
        /// Runs the callback method.
        /// </summary>
        /// <returns>The return value of the callback method.</returns>
        public object Callback()
        {
                #if UNITY_EDITOR
                        return m_buttonCallback?.DynamicInvoke();
                #else
                        return null;
                #endif
        }

        /// <summary>
        /// Checks if the validation method exists and returns the value from the method if it does.
        /// </summary>
        /// <returns>The value of the validation method if it exists, otherwise it returns true.</returns>
        public bool Validate()
        {
                #if UNITY_EDITOR
                        if (m_validationExists)
                        {
                                // If the validation method exists use that.
                                return (bool)m_validationDelegate.DynamicInvoke();
                        }
                        else
                        {
                                // If the validation method does not exist return true.
                                return true;
                        }
                #else
                        return true;
                #endif
                
        }

        /// <summary>
        /// Creates a button in the inspector on a bool field.
        /// </summary>
        /// <param name="callbackType">The Type of class the callback method is in.</param>
        /// <param name="callbackMethodName">The name of the callback method. Must be a static method that returns an object.</param>
        /// <param name="validationType">The Type of class the validation method is in. If it is null the type defaults to callbackType.</param>
        /// <param name="validationMethodName">The name of the validation method. Must be a static method that returns a bool. If it is null the button is always enabled.</param>
        public ButtonAttribute( Type callbackType, string callbackMethodName,Type validationType = null,string validationMethodName = null)
        {
                #if UNITY_EDITOR
                        if (callbackType == null )
                        {
                                Debug.LogError("callbackType must not be null!");
                                return;
                        }
                        if (callbackMethodName == null)
                        {
                                Debug.LogError("callbackMethodName must not be null!");
                                return;
                        }
        
                        MethodInfo callbackInfo = callbackType.GetMethod(callbackMethodName);
                        if (callbackInfo == null)
                        {
                                Debug.LogErrorFormat("Unable to get method info for callbackType {0} with callbackMethodName {1}",callbackType.ToString(),callbackMethodName);
                                return;
                        }
                        m_buttonCallback = Delegate.CreateDelegate(typeof(Func<object>),callbackInfo);
        
                        if (validationType == null)
                        {
                                validationType = callbackType;
                        }
        
                        if (validationMethodName == null)
                        {
                                return;
                        }
        
                        MethodInfo validationInfo = validationType.GetMethod(validationMethodName);
        
                        if (validationInfo == null)
                        {
                                return;
                        }
        
                        m_validationDelegate =  Delegate.CreateDelegate(typeof(Func<bool>), validationInfo);
                        if (m_validationDelegate != null)
                        {
                                m_validationExists = true;
                        }
                #endif
        }
}