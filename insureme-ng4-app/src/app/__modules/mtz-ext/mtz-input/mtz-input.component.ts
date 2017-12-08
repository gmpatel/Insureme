import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { isUndefined } from 'util';


@Component({
	selector: 'mtz-input',
	templateUrl: 'mtz-input.component.html',
	styleUrls: ['mtz-input.component.scss'],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => MtzInputComponent),
			multi: true
		}
	]
})
export class MtzInputComponent implements OnInit, ControlValueAccessor {

	@Input() type: string = 'text';
	@Input() placeholder: string;
	@Input() error: string;
	@Input() hint: string;
	@Input() required = false;

	@Output() validate = new EventEmitter();

	private _value = null;
	private propagateChange = (_: any) => {};

	get value(): any {
		return this._value;
	}

	set value(v: any) {
		if (v !== this._value) {
			this._value = v;
			this.propagateChange(this._value);
		}
	}

	private inputInvalid: boolean = false;

	constructor() {
	}

	private modPlaceHolder = null;

	ngOnInit() {
		this.modPlaceHolder = this.placeholder;
		if (this.required) {
			this.modPlaceHolder = this.placeholder + '*';
		}
	}

	writeValue(val: any): void {
		this.value = val;
	}

	registerOnChange(fn: any): void {
		this.propagateChange = fn;
	}

	registerOnTouched(fn: any): void {}

	private noValue(): boolean {
		return this._value == '' || this._value == null || this._value == 'null' || isUndefined(this._value);
	}

	protected onFocus() {
		this.inputInvalid = false;
	}

	private validateEmail(): boolean {
		var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
		return re.test(this.value);
	}


	private basicErrorCheck(): string {
		if (this.required && this.noValue()) {
			return this.placeholder + ' is required ...';
		}

		if (this.type == 'email') {
			if (!this.validateEmail()) {
				return 'Email is not valid ...';
			}
		}

		return null;
	}

	protected onBlur() {
		let error = this.basicErrorCheck();
		if (error) {
			let val = this._value;
			this.value = '';
			this._value = val;
			this.inputInvalid = true;
			this.error = error;
		} else {
			this.inputInvalid = false;
			this.error = null;
			this.validate.emit(this.value);
		}
		this.propagateChange(this.value);
	}
}
