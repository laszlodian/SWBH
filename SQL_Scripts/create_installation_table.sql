-- Table: public.installation

-- DROP TABLE public.installation;

CREATE TABLE public.installation
(
    pk_id integer NOT NULL DEFAULT nextval('installation_pk_id_seq'::regclass),
    user_name character varying COLLATE pg_catalog."default",
    machine_name character varying COLLATE pg_catalog."default",
    install_date date,
    CONSTRAINT installation_pkey PRIMARY KEY (pk_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.installation
    OWNER to swb_installs_admin;