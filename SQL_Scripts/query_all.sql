select * from packages left join installation on packages.fk_installations_id = installation.pk_id 
left join sunriseworkbench on sunriseworkbench.fk_installation_id = installation.pk_id