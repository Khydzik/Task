import {Injectable} from '@angular/core';
import {ToastrService} from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ShowToastrService {
  constructor (private toastr: ToastrService) {}

  showError (message: string, error?) {
    this.toastr.error(message);
    console.error(error);
  }

  showSuccess (message: string) {
    this.toastr.success(message);
  }
}
