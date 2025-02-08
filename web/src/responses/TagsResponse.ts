import Tag from '../models/Tag';

/** Defines a tags response model. */
export default interface TagsResponse {
	/** Array of years. */
	tags: Tag[];
}