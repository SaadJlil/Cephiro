CREATE TABLE personas (
    persona_id UUID,
    user_id UUID,
    pseudonym TEXT,
    avatar_url TEXT WITH NULL,
    PRIMARY KEY (user_id, persona_id, pseudonym)
    UNIQUE (pseudonym, avatar_url)
) WITH CLUSTERING ORDER BY (pseudonym ASC)

CREATE TABLE persona_profile (
    persona_id UUID,
    persona_profile_id UUID,
    chara_designs LIST<TEXT>
    about_me TEXT WITH NULL,
    history TEXT WITH NULL,
    powers LIST<MAP<TEXT, TEXT>> WITH NULL,
    PRIMARY KEY((persona_id) persona_profile_id)
)

CREATE TABLE relationships (
    persona_id UUID,
    persona_pseudo TEXT;
    friend_id UUID NOT NULL;
    friend_pseudo TEXT NOT NULL,
    PRIMARY KEY (persona_id, persona_pseudo)
) WITH CLUSTERING ORDER BY (persona_pseudo ASC, friend_pseudo ASC);

CREATE TABLE channels (
    channel_id UUID,
    owner_id UUID,
    channel_name TEXT NOT NULL,
    is_private BOOLEAN,
    PRIMARY KEY (channel_id, owner_id)
) WITH CLUSTERING ORDER BY (channel_name ASC);

CREATE TABLE channel_bans (
    channel_id UUID,
    banned_id UUID,
    PRIMARY KEY (channel_id, banned_id)
);

CREATE TABLE channel_tags (
    channel_tags_id UUID,
    channel_id UUID,
    tag_name TEXT NOT NULL,
    allow_ban BOOLEAN,
    allow_invite BOOLEAN,
    create_chats BOOLEAN,
    PRIMARY KEY (channel_id, channel_tags_id)
    UNIQUE (tag_name)
) WITH CLUSTERING ORDER BY (tag_name)

CREATE TABLE memberships (
    membership_id TIMEUUID,
    channel_id UUID,
    persona_id UUID,
    persona_name TEXT NOT NULL,
    tags SET<TEXT> WITH NULL,
    PRIMARY KEY ((channel_id, persona_id) membership_id)
);

CREATE TABLE chats (
    channel_id UUID,
    chat_id UUID,
    chat_name TEXT NOT NULL,
    is_private BOOLEAN,
    allowed_tags SET<TEXT>,
    PRIMARY KEY (chat_id, channel_id)
    UNIQUE (chat_name)
) WITH CLUSTERING ORDER BY (chat_name ASC);

CREATE TABLE groups (
    channel_id UUID,
    group_id UUID,
    group_name TEXT NOT NULL,
    chat_ids SET<UUID>
    chat_names SET<name>
    PRIMARY KEY (channel_id, group_id)
    UNIQUE (group_name)
);

CREATE TABLE chat_post (
    channel_id UUID,
    chat_id UUID,
    post_id TIMEUUID,
    post_content TEXT NOT NULL,
    persona_id UUID,
    persona_name TEXT,
    avatar_url TEXT,
    update_date TIMESTAMP,
    PRIMARY KEY ((channel_id, chat_id), post_id)
);