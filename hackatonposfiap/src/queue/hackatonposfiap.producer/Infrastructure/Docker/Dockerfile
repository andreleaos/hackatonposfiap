FROM rabbitmq:3.9-management

ENV RABBITMQ_DEFAULT_USER=hackauser
ENV RABBITMQ_DEFAULT_PASS=hackauser
ENV RABBITMQ_DEFAULT_VHOST=/

COPY rabbitmq.config /etc/rabbitmq/

RUN rabbitmq-plugins enable --offline rabbitmq_management rabbitmq_management_agent rabbitmq_management_visualiser rabbitmq_shovel rabbitmq_shovel_management rabbitmq_federation rabbitmq_federation_management rabbitmq_stomp rabbitmq_web_stomp rabbitmq_prometheus rabbitmq_peer_discovery_aws
RUN rabbitmqctl set_policy ha-all "" '{"ha-mode":"all","ha-sync-mode":"automatic"}'

# Expor as portas
EXPOSE 5672 15672
