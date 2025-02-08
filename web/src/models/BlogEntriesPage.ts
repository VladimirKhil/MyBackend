import BlogEntry from './BlogEntry';

export default interface BlogEntriesPage {
	entries: BlogEntry[];
	totalCount: number;
}