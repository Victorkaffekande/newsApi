# newsApi
Does not run in docker i don't know why.

## examples:
### Login getting a token
Post http://localhost:44344//Auth

{
"email": "writer@mail.com",
"password": "passwrd"
}

other valid emails are editor@mail.com and subscriber@mail.com

### creating a comment
post https://localhost:44344/Comment/createComment

{
"content": "my first comment",
"articleId": 1
}
Must have have a jwt from a subscribed user in the auth header

### create article
post https://localhost:44344/article/createArticle 

{
"title": "Test title",
"content": "test contentasdasda"
}
 must have a jwt from a writer

### update article
PUT https://localhost:44344/article/updateArticle
id is the articleId. I am bad at naming thins :))
{
"id": 2,
"title": "this has been edited by bigman",
"content": "test contentasdasda"
}
must have a jwt from an editor or the writer who owns the article