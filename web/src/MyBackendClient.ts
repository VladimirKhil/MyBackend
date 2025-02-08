import BlogEntriesPage from './models/BlogEntriesPage';
import BlogEntry from './models/BlogEntry';
import NewsItem from './models/NewsItem';
import Tag from './models/Tag';
import MyBackendClientOptions from './MyBackendClientOptions';
import NewsResponse from './responses/NewsResponse';
import TagsResponse from './responses/TagsResponse';
import YearsResponse from './responses/YearsResponse';

/** Defines MyBackend service client. */
export default class MyBackendClient {
	/**
	 * Initializes a new instance of MyBackendClient class.
	 * @param options Client options.
	 */
	constructor(public options: MyBackendClientOptions) { }

	/** Gets all news years. */
	async getNewsYearAsync(): Promise<number[]> {
		return (await this.getAsync<YearsResponse>('news/years')).years;
	}

	async getNewsByYearAsync(year: number): Promise<NewsItem[]> {
		return (await this.getAsync<NewsResponse>(`news/years/${year}`)).news;
	}

	async getTagsAsync(): Promise<Tag[]> {
		return (await this.getAsync<TagsResponse>('tags')).tags;
	}

	getBlogEntriesAsync(tagId?: number, from = 0, count = 10): Promise<BlogEntriesPage> {
		const query = tagId ? `tagId=${tagId}&` : '';
		return this.getAsync<BlogEntriesPage>(`blog?${query}from=${from}&count=${count}`);
	}

	getBlogEntryAsync(id: number): Promise<BlogEntry> {
		return this.getAsync<BlogEntry>(`blog/${id}`);
	}

	/**
	 * Gets resource by Uri.
	 * @param uri Resource Uri.
	 */
	async getAsync<T>(uri: string) {
		const response = await fetch(`${this.options.serviceUri}/api/v1/${uri}`, this.options.culture ? {
			headers: {
				'Accept-Language': this.options.culture
			}
		} : undefined);

		if (!response.ok) {
			throw new Error(`Error while retrieving ${uri}: ${response.status} ${await response.text()}`);
		}

		return <T>(await response.json());
	}
}