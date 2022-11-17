create table user_info(
    id varchar(20) primary key,
    name varchar(20),
    password varchar(20),
    avatar_url varchar(100),
    introduction text,
    following_count bigint,
    follower_count bigint,
    favorite_count bigint
);

create table user_following(
    follower_id varchar(20),
    followee_id varchar(20),
    primary key(follower_id, followee_id)
);

create table content_media(
    content_id bigint,
    media_url varchar(100),
    primary key(content_id, media_url)
);

create table comment_user_liked(
    comment_id bigint,
    user_id varchar(20),
    primary key(comment_id, user_id)
);

create table comment(
    id bigint primary key auto_increment,
    user_id varchar(20),
    content text,
    like_count bigint
);

create table content_comment(
    content_id bigint,
    comment_id bigint,
    primary key(content_id, comment_id)
);

create table content_user_interaction(
    content_id bigint,
    user_id varchar(20),
    liked boolean,
    favorite boolean,
    primary key(content_id, user_id)
);

create table content(
    id bigint primary key auto_increment,
    title text,
    author_id varchar(20),
    view_count bigint,
    like_count bigint,
    favorite_count bigint,
    publish_time bigint,
    body text
);