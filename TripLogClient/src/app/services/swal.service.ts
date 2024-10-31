import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  constructor() { }

  callToast(title:string,icon:SweetAlertIcon="info"){
    Swal.fire({
      title:title,
      timer:2000,
      icon:icon,
      position:'bottom-right',
      showCancelButton:false,
      showCloseButton:true,
      showConfirmButton:false,
      toast:true
    })
  }

  callSwal(title:string,text:string,callback:()=>void){
    Swal.fire({
      title:title,
      text:text,
      icon:'question',
      showCancelButton:true,
      cancelButtonText:"İptal",
      showConfirmButton:true,
      confirmButtonText:"Sil",
    }).then( res => {
      if(res.isConfirmed){
        callback();
      }
    } )
  }
}

export type SweetAlertIcon = 'success' | 'error' | 'warning' | 'info' | 'question'