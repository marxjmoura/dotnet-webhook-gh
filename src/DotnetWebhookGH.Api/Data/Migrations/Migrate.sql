CREATE TABLE IF NOT EXISTS __ef_migration_history (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT "PK___ef_migration_history" PRIMARY KEY (migration_id)
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public.label (
        id bigint NOT NULL,
        name character varying(50) NOT NULL,
        color character varying(6) NULL,
        description character varying(100) NULL,
        is_default boolean NOT NULL,
        CONSTRAINT "PK_label" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public."user" (
        id bigint NOT NULL,
        login character varying(50) NOT NULL,
        CONSTRAINT "PK_user" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public.repository (
        id bigint NOT NULL,
        owner_id bigint NOT NULL,
        name character varying(100) NOT NULL,
        description text NULL,
        language text NULL,
        default_branch text NULL,
        topics text NULL,
        is_private boolean NOT NULL,
        is_fork boolean NOT NULL,
        CONSTRAINT "PK_repository" PRIMARY KEY (id),
        CONSTRAINT "FK_repository_user_owner_id" FOREIGN KEY (owner_id) REFERENCES public."user" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public.issue (
        delivery text NOT NULL,
        event text NOT NULL,
        number integer NOT NULL,
        repository_id bigint NOT NULL,
        sender_id bigint NOT NULL,
        title character varying(1024) NOT NULL,
        body text NULL,
        state text NOT NULL,
        locked boolean NOT NULL,
        created_at timestamp with time zone NOT NULL,
        updated_at timestamp with time zone NOT NULL,
        closed_at timestamp with time zone NULL,
        CONSTRAINT "PK_issue" PRIMARY KEY (delivery),
        CONSTRAINT "FK_issue_repository_repository_id" FOREIGN KEY (repository_id) REFERENCES public.repository (id) ON DELETE CASCADE,
        CONSTRAINT "FK_issue_user_sender_id" FOREIGN KEY (sender_id) REFERENCES public."user" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public.issue_assignee (
        issue_delivery text NOT NULL,
        user_id bigint NOT NULL,
        CONSTRAINT "PK_issue_assignee" PRIMARY KEY (issue_delivery, user_id),
        CONSTRAINT "FK_issue_assignee_issue_issue_delivery" FOREIGN KEY (issue_delivery) REFERENCES public.issue (delivery) ON DELETE CASCADE,
        CONSTRAINT "FK_issue_assignee_user_user_id" FOREIGN KEY (user_id) REFERENCES public."user" (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public.issue_label (
        issue_delivery text NOT NULL,
        label_id bigint NOT NULL,
        CONSTRAINT "PK_issue_label" PRIMARY KEY (issue_delivery, label_id),
        CONSTRAINT "FK_issue_label_issue_issue_delivery" FOREIGN KEY (issue_delivery) REFERENCES public.issue (delivery) ON DELETE CASCADE,
        CONSTRAINT "FK_issue_label_label_label_id" FOREIGN KEY (label_id) REFERENCES public.label (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE TABLE public.reaction (
        delivery text NOT NULL,
        total_count integer NOT NULL,
        one_plus integer NOT NULL,
        one_minus integer NOT NULL,
        laugh integer NOT NULL,
        hooray integer NOT NULL,
        confused integer NOT NULL,
        heart integer NOT NULL,
        rocket integer NOT NULL,
        eyes integer NOT NULL,
        CONSTRAINT "PK_reaction" PRIMARY KEY (delivery),
        CONSTRAINT "FK_reaction_issue_delivery" FOREIGN KEY (delivery) REFERENCES public.issue (delivery) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_issue_number" ON public.issue (number);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_issue_repository_id" ON public.issue (repository_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_issue_sender_id" ON public.issue (sender_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_issue_updated_at" ON public.issue (updated_at);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_issue_assignee_user_id" ON public.issue_assignee (user_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_issue_label_label_id" ON public.issue_label (label_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_repository_name" ON public.repository (name);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_repository_owner_id" ON public.repository (owner_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    CREATE INDEX "IX_user_login" ON public."user" (login);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM __ef_migration_history WHERE "migration_id" = '20230219150143_InitialCreate') THEN
    INSERT INTO __ef_migration_history (migration_id, product_version)
    VALUES ('20230219150143_InitialCreate', '6.0.14');
    END IF;
END $EF$;

COMMIT;
