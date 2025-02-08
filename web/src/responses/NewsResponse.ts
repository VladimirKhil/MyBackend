import NewsItem from '../models/NewsItem';

/** Defines a news response model. */
export default interface NewsResponse {
	/** Array of news. */
	news: NewsItem[];
}