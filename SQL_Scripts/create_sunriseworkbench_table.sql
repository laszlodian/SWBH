-- Table: public.sunriseworkbench

-- DROP TABLE public.sunriseworkbench;

CREATE TABLE public.sunriseworkbench
(
    pk_id integer NOT NULL DEFAULT nextval('sunriseworkbench_pk_id_seq'::regclass),
    fk_installation_id integer NOT NULL,
    swb_version character varying COLLATE pg_catalog."default",
    CONSTRAINT sunriseworkbench_pkey PRIMARY KEY (pk_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.sunriseworkbench
    OWNER to swb_installs_admin;