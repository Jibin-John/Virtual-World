//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vx_evt_network_message_t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal vx_evt_network_message_t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(vx_evt_network_message_t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~vx_evt_network_message_t() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VivoxCoreInstancePINVOKE.delete_vx_evt_network_message_t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public vx_evt_base_t base_ {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_base__set(swigCPtr, vx_evt_base_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_base__get(swigCPtr);
      vx_evt_base_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new vx_evt_base_t(cPtr, false);
      return ret;
    } 
  }

  public string account_handle {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_account_handle_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_account_handle_get(swigCPtr);
      return ret;
    } 
  }

  public vx_evt_network_message_type network_message_type {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_network_message_type_set(swigCPtr, (int)value);
    } 
    get {
      vx_evt_network_message_type ret = (vx_evt_network_message_type)VivoxCoreInstancePINVOKE.vx_evt_network_message_t_network_message_type_get(swigCPtr);
      return ret;
    } 
  }

  public string content_type {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_content_type_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_content_type_get(swigCPtr);
      return ret;
    } 
  }

  public string content {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_content_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_content_get(swigCPtr);
      return ret;
    } 
  }

  public string sender_uri {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_sender_uri_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_sender_uri_get(swigCPtr);
      return ret;
    } 
  }

  public string sender_display_name {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_sender_display_name_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_sender_display_name_get(swigCPtr);
      return ret;
    } 
  }

  public string sender_alias_username {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_sender_alias_username_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_sender_alias_username_get(swigCPtr);
      return ret;
    } 
  }

  public string receiver_alias_username {
    set {
      VivoxCoreInstancePINVOKE.vx_evt_network_message_t_receiver_alias_username_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_evt_network_message_t_receiver_alias_username_get(swigCPtr);
      return ret;
    } 
  }

  public vx_evt_network_message_t() : this(VivoxCoreInstancePINVOKE.new_vx_evt_network_message_t(), true) {
  }

}
