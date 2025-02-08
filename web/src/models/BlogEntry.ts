import Tag from './Tag';

export default interface BlogEntry {
	id: number;
	dateTime: Date;
	title: string;
	text: string;
	tags: Tag[];
}