import MyBackendClient from '../src/MyBackendClient';
import MyBackendClientOptions from '../src/MyBackendClientOptions';

const options: MyBackendClientOptions = {
	serviceUri: 'http://vladimirkhil.com/backend'
};

const myBackendClient = new MyBackendClient(options);

test('Get news years', async () => {
	const years = await myBackendClient.getNewsYearAsync();
	expect(years.length).toBeGreaterThan(10);
});

test('Get news by year', async () => {
	const news = await myBackendClient.getNewsByYearAsync(2020);
	expect(news.length).toBeGreaterThan(5);
});

test('Get tags', async () => {
	const tags = await myBackendClient.getTagsAsync();
	expect(tags.length).toBeGreaterThan(0);
});

test('Get blog entries', async () => {
	const blogEntries = await myBackendClient.getBlogEntriesAsync();
	expect(blogEntries.entries.length).toBeGreaterThan(0);
});

test('Get blog entries by tag', async () => {
	const tags = await myBackendClient.getTagsAsync();
	const blogEntries = await myBackendClient.getBlogEntriesAsync(tags[0].id);
	expect(blogEntries.entries.length).toBeGreaterThan(0);
});