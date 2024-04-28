docker rm itm.service.api

docker run 	--name itm.service.api^
			--add-host=itm_local_sql:192.168.0.248^
			--env ServiceConfig__DalInitParams__ConnectionString="Data Source=itm_local_sql; Initial Catalog=InsidersTradeMonitor; User ID=itm_svc_api; Password=ITMDataImporterService2022!"^
			-it -p 8082:8082 globus000/itm.service.api