-- Table: public.packages

-- DROP TABLE public.packages;

CREATE TABLE public.packages
(
    pk_id integer NOT NULL DEFAULT nextval('packages_pk_id_seq'::regclass),
    fk_installations_id integer NOT NULL,
    package_name character varying COLLATE pg_catalog."default",
    package_version character varying COLLATE pg_catalog."default",
    CONSTRAINT packages_pkey PRIMARY KEY (pk_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.packages
    OWNER to swb_installs_admin;