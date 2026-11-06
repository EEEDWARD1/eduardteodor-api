# Local sample data

Run from the `EduardTeodor` directory with your local `.env` configured:

```bash
docker compose up -d --wait db
docker compose run --rm migrate
docker compose exec -T db sh -c 'psql -v ON_ERROR_STOP=1 -U "$POSTGRES_USER" -d "$POSTGRES_DB"' < scripts/seed-local.sql
```

The seed runs in a transaction and can be repeated without duplicating its rows.
It inserts five posts and four tags; it is not run automatically during deployment.
Rider's development connection string must match the local database credentials.

With the API running in Rider, try:

| Request | Sample results |
| --- | --- |
| `/v1/posts` | All five posts |
| `/v1/posts?postType=Project` | Two projects |
| `/v1/posts?tag=dotnet` | One project and one thought |
| `/v1/posts?postType=Project&tag=dotnet` | Demo Content API |
| `/v1/posts?tag=1` | Empty array |
| `/v1/posts/demo-content-api` | Demo Content API |
| `/v1/posts/demo-untagged-thought` | Post with an empty tag list |
| `/v1/posts/does-not-exist` | 404 |

Other existing local posts may also appear in list results.
