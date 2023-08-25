import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-custom-validator',
  templateUrl: './custom-validator.component.html',
  styleUrls: ['./custom-validator.component.css']
})
export class CustomValidatorComponent {
  @Input() formControl: FormControl = new FormControl();
  @Input() fieldName: string = 'This field';
  @Input() minMsg: string = 'is too small number!';
  @Input() maxMsg: string = 'is too large number!' ;
  @Input() minLengthMsg: string = 'is too short value!' ;
  @Input() maxLengthMsg: string = 'is too long value!' ;
  @Input() requiredMsg: string = 'is required!';

  constructor() {

  }
}
