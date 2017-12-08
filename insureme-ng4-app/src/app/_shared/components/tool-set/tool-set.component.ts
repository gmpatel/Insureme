import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
	selector: 'tool-set',
	templateUrl: './tool-set.component.html',
	styleUrls: ['./tool-set.component.scss']
})
export class ToolSetComponent implements OnInit {

	@Input() tools: string[];
	@Output() toolClickHandler = new EventEmitter();

	constructor() {
	}

	ngOnInit() {
	}

	protected toolClick(tool) {
		this.toolClickHandler.emit(tool);
	}
}
