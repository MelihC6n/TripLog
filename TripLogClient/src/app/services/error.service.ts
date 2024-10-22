import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(
    private Swal:SwalService
  ) { }

  errorHandler(err:HttpErrorResponse){
    let message="Error";

    if(err.status===0){
      message+=" : API is not avaliable!";
    }
    else if(err.status===401){
      message+=" : You are not authorized!";
    }
    else if(err.status===404){
      message+=" : API is not found!"
    }
    else if(err.status===500){
      message=" : ";
      for(const e of err.error.errorMessages){
        message += e +"\n";
      }
    }

    this.Swal.callToast("message",'error');
  }
}
