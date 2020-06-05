//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vx_req_account_authtoken_login_t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal vx_req_account_authtoken_login_t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(vx_req_account_authtoken_login_t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~vx_req_account_authtoken_login_t() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VivoxCoreInstancePINVOKE.delete_vx_req_account_authtoken_login_t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

        public static implicit operator vx_req_base_t(vx_req_account_authtoken_login_t t) {
            return t.base_;
        }
        
  public vx_req_base_t base_ {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_base__set(swigCPtr, vx_req_base_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_base__get(swigCPtr);
      vx_req_base_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new vx_req_base_t(cPtr, false);
      return ret;
    } 
  }

  public string connector_handle {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_connector_handle_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_connector_handle_get(swigCPtr);
      return ret;
    } 
  }

  public string authtoken {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_authtoken_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_authtoken_get(swigCPtr);
      return ret;
    } 
  }

  public vx_text_mode enable_text {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_enable_text_set(swigCPtr, (int)value);
    } 
    get {
      vx_text_mode ret = (vx_text_mode)VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_enable_text_get(swigCPtr);
      return ret;
    } 
  }

  public int participant_property_frequency {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_participant_property_frequency_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_participant_property_frequency_get(swigCPtr);
      return ret;
    } 
  }

  public int enable_buddies_and_presence {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_enable_buddies_and_presence_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_enable_buddies_and_presence_get(swigCPtr);
      return ret;
    } 
  }

  public vx_buddy_management_mode buddy_management_mode {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_buddy_management_mode_set(swigCPtr, (int)value);
    } 
    get {
      vx_buddy_management_mode ret = (vx_buddy_management_mode)VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_buddy_management_mode_get(swigCPtr);
      return ret;
    } 
  }

  public int autopost_crash_dumps {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_autopost_crash_dumps_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_autopost_crash_dumps_get(swigCPtr);
      return ret;
    } 
  }

  public string acct_mgmt_server {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_acct_mgmt_server_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_acct_mgmt_server_get(swigCPtr);
      return ret;
    } 
  }

  public string application_token {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_application_token_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_application_token_get(swigCPtr);
      return ret;
    } 
  }

  public string application_override {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_application_override_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_application_override_get(swigCPtr);
      return ret;
    } 
  }

  public vx_session_answer_mode answer_mode {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_answer_mode_set(swigCPtr, (int)value);
    } 
    get {
      vx_session_answer_mode ret = (vx_session_answer_mode)VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_answer_mode_get(swigCPtr);
      return ret;
    } 
  }

  public string client_ip_override {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_client_ip_override_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_client_ip_override_get(swigCPtr);
      return ret;
    } 
  }

  public int enable_presence_persistence {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_enable_presence_persistence_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_enable_presence_persistence_get(swigCPtr);
      return ret;
    } 
  }

  public string account_handle {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_account_handle_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_authtoken_login_t_account_handle_get(swigCPtr);
      return ret;
    } 
  }

  public vx_req_account_authtoken_login_t() : this(VivoxCoreInstancePINVOKE.new_vx_req_account_authtoken_login_t(), true) {
  }

}
