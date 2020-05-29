-- Table: public.packages

-- DROP TABLE public.packages;

CREATE TABLE public.packages
(
    pk_id serial NOT NULL,
    package_name character varying COLLATE pg_catalog."default",
    package_version character varying COLLATE pg_catalog."default",
    fk_swb_installs_id integer NOT NULL,
    CONSTRAINT packages_pkey PRIMARY KEY (pk_id)
)

TABLESPACE pg_default;

ALTER TABLE public.packages
    OWNER to postgres;