-- Local development only. Apply migrations before running this script.
-- Fixed IDs make reruns safe without replacing existing posts.
BEGIN;

INSERT INTO tags (name)
VALUES ('dotnet'), ('backend'), ('nextjs'), ('docker')
ON CONFLICT (name) DO NOTHING;

INSERT INTO posts (id, post_type, slug, title, description, image, content, href)
VALUES
    ('019a4a00-0000-7000-8000-000000000001', 'project', 'demo-content-api',
     'Demo Content API', 'A sample API built with ASP.NET Core and PostgreSQL.',
     NULL, '# Demo Content API' || chr(10) || 'A local sample project for testing post responses.',
     'https://example.com/content-api'),
    ('019a4a00-0000-7000-8000-000000000002', 'project', 'demo-portfolio',
     'Demo Portfolio', 'A sample portfolio built with Next.js.',
     'https://placehold.co/800x450', 'A portfolio with project pages and a thoughts section.',
     'https://example.com/portfolio'),
    ('019a4a00-0000-7000-8000-000000000003', 'thought', 'demo-simple-services',
     'Keeping Services Simple', 'Some thoughts on using EF Core directly in services.',
     NULL, 'Start with an endpoint and a service. Add abstractions when they solve a concrete problem.',
     NULL),
    ('019a4a00-0000-7000-8000-000000000004', 'thought', 'demo-container-notes',
     'Container Notes', 'Local notes on containers and database migrations.',
     NULL, 'Restore dependencies, apply migrations, and then start the application.',
     NULL),
    ('019a4a00-0000-7000-8000-000000000005', 'thought', 'demo-untagged-thought',
     'An Untagged Thought', 'A sample post with no tags or optional links.',
     NULL, 'Use this post to check that an empty tag list is returned correctly.', NULL)
ON CONFLICT (id) DO NOTHING;

INSERT INTO post_tags (posts_id, tags_id)
SELECT sample.post_id::uuid, tags.id
FROM (VALUES
    ('019a4a00-0000-7000-8000-000000000001', 'dotnet'),
    ('019a4a00-0000-7000-8000-000000000001', 'backend'),
    ('019a4a00-0000-7000-8000-000000000002', 'nextjs'),
    ('019a4a00-0000-7000-8000-000000000003', 'dotnet'),
    ('019a4a00-0000-7000-8000-000000000003', 'backend'),
    ('019a4a00-0000-7000-8000-000000000004', 'docker')
) AS sample(post_id, tag_name)
JOIN tags ON tags.name = sample.tag_name
ON CONFLICT (posts_id, tags_id) DO NOTHING;

COMMIT;
