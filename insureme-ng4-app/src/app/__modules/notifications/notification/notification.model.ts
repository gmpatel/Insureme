export interface Notification {
	id: number;
	type: string;
	title?: string;
	message: string;
	sticky?: boolean
}