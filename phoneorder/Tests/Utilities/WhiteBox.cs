/*
 * User: Adam
 * Date: 9/9/2016
 * Time: 11:26 AM
 * 
 */

using System;
using System.Globalization;
using System.Reflection;

namespace PhoneOrder.Tests {
	/// <summary>
	/// Test Helper WhiteBox Methods for setting values with Reflection
	/// </summary>
	public static class WhiteBox {
		
	    /// <summary>
	    /// Returns a private Property Value from the Object.
	    /// Throws a ArgumentOutOfRangeException if the Property is not found.
	    /// </summary>
	    /// <typeparam name="T">Type of the Property</typeparam>
	    /// <param name="containingInstance">Instance of the Object from where the Property Value is returned</param>
	    /// <param name="propName">Propertyname as a string.</param>
	    /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
	    /// <returns>PropertyValue</returns>
	    public static T GetPrivateProperty<T>(this object containingInstance, string propName) {
	        if (containingInstance == null) throw new ArgumentNullException("containingInstance");
	        PropertyInfo pi = containingInstance.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
	        if (pi == null) throw new ArgumentOutOfRangeException("propName", string.Format(CultureInfo.InvariantCulture, "Property {0} was not found in Type {1}", propName, containingInstance.GetType().FullName));
	        return (T)pi.GetValue(containingInstance, null);
	    }
	
	    /// <summary>
	    /// Returns a private Field Value from the Object.
	    /// </summary>
	    /// <typeparam name="T">Type of the Property</typeparam>
	    /// <param name="containingInstance">Instance of the Object from where the Property Value is returned</param>
	    /// <param name="propName">Propertyname as a string.</param>
	    /// <returns>PropertyValue</returns>
	    /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
	    public static T GetPrivateField<T>(this object containingInstance, string propName) {
	        if (containingInstance == null) throw new ArgumentNullException("containingInstance");
	        Type t = containingInstance.GetType();
	        FieldInfo fi = null;
	        while (fi == null && t != null) {
	            fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
	            t = t.BaseType;
	        }
	        if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format(CultureInfo.InvariantCulture, "Field {0} was not found in Type {1}", propName, containingInstance.GetType().FullName));
	        return (T)fi.GetValue(containingInstance);
	    }
	
	   /// <summary>
	    /// Sets a private Property Value of the Object.
	    /// Throws a ArgumentOutOfRangeException if the Property is not found.
	    /// </summary>
	    /// <typeparam name="T">Type of the Property</typeparam>
	    /// <param name="containingInstance">Instance of the Object from where the Property Value will be set</param>
	    /// <param name="propName">Propertyname as a string.</param>
	    /// <param name="newValue">New value to set.</param>
	    /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
	    public static void SetPrivateProperty<T>(this object containingInstance, string propName, T newValue) {
	        Type t = containingInstance.GetType();
	        if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static) == null)
	            throw new ArgumentOutOfRangeException("propName", string.Format(CultureInfo.InvariantCulture, "Property {0} was not found in Type {1}", propName, containingInstance.GetType().FullName));
	        t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, containingInstance, new object[] { newValue }, CultureInfo.InvariantCulture);
	    }
	
	    /// <summary>
	    /// Set a private Field Value of the Object.
	    /// </summary>
	    /// <typeparam name="T">Type of the Property</typeparam>
	    /// <param name="containingInstance">Instance of the Object from where the Property Value will be set</param>
	    /// <param name="propName">Propertyname as a string.</param>
	    /// <param name="newValue">New value to set.</param>
	    /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
	    public static void SetPrivateField<T>(this object containingInstance, string propName, T newValue) {
	        if (containingInstance == null) throw new ArgumentNullException("containingInstance");
	        Type t = containingInstance.GetType();
	        FieldInfo fi = null;
	        while (fi == null && t != null) {
	            fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
	            t = t.BaseType;
	        }
	        if (fi == null) throw new ArgumentOutOfRangeException("propName", string.Format(CultureInfo.InvariantCulture, "Field {0} was not found in Type {1}", propName, containingInstance.GetType().FullName));
	        fi.SetValue(containingInstance, newValue);
	    }
	}
}
