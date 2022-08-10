--
-- PostgreSQL database cluster dump
--

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Drop databases (except postgres and template1)
--

DROP DATABASE carreservation;




--
-- Drop roles
--

DROP ROLE carreservationadmin;


--
-- Roles
--

CREATE ROLE carreservationadmin;
ALTER ROLE carreservationadmin WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS PASSWORD 'SCRAM-SHA-256$4096:vI3Uq/rqUqsr1z6oY7eJTA==$WimUzc5ryw6tcqNdBzL5Rr1hdOeWXEUf8i5Z3MJ2KeI=:zE6kjz4kb0ueaaMDO35M7KC+CapmnjlwoElAwpWLG/Q=';






--
-- Databases
--

--
-- Database "template1" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4 (Debian 14.4-1.pgdg110+1)
-- Dumped by pg_dump version 14.4 (Debian 14.4-1.pgdg110+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

UPDATE pg_catalog.pg_database SET datistemplate = false WHERE datname = 'template1';
DROP DATABASE template1;
--
-- Name: template1; Type: DATABASE; Schema: -; Owner: carreservationadmin
--

CREATE DATABASE template1 WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE template1 OWNER TO carreservationadmin;

\connect template1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: DATABASE template1; Type: COMMENT; Schema: -; Owner: carreservationadmin
--

COMMENT ON DATABASE template1 IS 'default template for new databases';


--
-- Name: template1; Type: DATABASE PROPERTIES; Schema: -; Owner: carreservationadmin
--

ALTER DATABASE template1 IS_TEMPLATE = true;


\connect template1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: DATABASE template1; Type: ACL; Schema: -; Owner: carreservationadmin
--

REVOKE CONNECT,TEMPORARY ON DATABASE template1 FROM PUBLIC;
GRANT CONNECT ON DATABASE template1 TO PUBLIC;


--
-- PostgreSQL database dump complete
--

--
-- Database "carreservation" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4 (Debian 14.4-1.pgdg110+1)
-- Dumped by pg_dump version 14.4 (Debian 14.4-1.pgdg110+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: carreservation; Type: DATABASE; Schema: -; Owner: carreservationadmin
--

CREATE DATABASE carreservation WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE carreservation OWNER TO carreservationadmin;

\connect carreservation

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: cars; Type: TABLE; Schema: public; Owner: carreservationadmin
--

CREATE TABLE public.cars (
    car_id integer NOT NULL,
    serial_number character varying(50) NOT NULL,
    model character varying(50) NOT NULL,
    status boolean NOT NULL,
    insert_date timestamp(0) with time zone NOT NULL,
    maker_id integer NOT NULL
);


ALTER TABLE public.cars OWNER TO carreservationadmin;

--
-- Name: cars_car_id_seq; Type: SEQUENCE; Schema: public; Owner: carreservationadmin
--

ALTER TABLE public.cars ALTER COLUMN car_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.cars_car_id_seq
    START WITH 10
    INCREMENT BY 1
    MINVALUE 10
    NO MAXVALUE
    CACHE 1
);


--
-- Name: cars_makers; Type: TABLE; Schema: public; Owner: carreservationadmin
--

CREATE TABLE public.cars_makers (
    car_maker_id integer NOT NULL,
    car_maker_name character varying(50) NOT NULL
);


ALTER TABLE public.cars_makers OWNER TO carreservationadmin;

--
-- Name: reserved_cars; Type: TABLE; Schema: public; Owner: carreservationadmin
--

CREATE TABLE public.reserved_cars (
    reserved_car_id integer NOT NULL,
    car_id integer NOT NULL,
    user_id integer NOT NULL,
    reservation_date timestamp(0) with time zone NOT NULL,
    reservation_minutes integer NOT NULL
);


ALTER TABLE public.reserved_cars OWNER TO carreservationadmin;

--
-- Name: reserved_cars_reserved_car_id_seq; Type: SEQUENCE; Schema: public; Owner: carreservationadmin
--

ALTER TABLE public.reserved_cars ALTER COLUMN reserved_car_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.reserved_cars_reserved_car_id_seq
    START WITH 10
    INCREMENT BY 1
    MINVALUE 10
    NO MAXVALUE
    CACHE 1
);


--
-- Name: users; Type: TABLE; Schema: public; Owner: carreservationadmin
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    user_name character varying(100) NOT NULL,
    login character varying(100) NOT NULL,
    password character varying(100) NOT NULL
);


ALTER TABLE public.users OWNER TO carreservationadmin;

--
-- Data for Name: cars; Type: TABLE DATA; Schema: public; Owner: carreservationadmin
--

COPY public.cars (car_id, serial_number, model, status, insert_date, maker_id) FROM stdin;
3	C3	Mercedes GTC	t	2022-08-07 21:30:03+00	1
4	C4	Mercedes GT Roadcaster	t	2022-08-07 21:30:27+00	1
5	C5	Mercedes GLC63	t	2022-08-07 21:30:49+00	1
6	C6	Hyundai IONIQ 6	t	2022-08-07 21:31:57+00	3
7	C7	Hyundai IONIQ 5	t	2022-08-07 21:32:11+00	3
8	C8	Hyundai AZERA	t	2022-08-07 21:32:25+00	3
9	C9	Hyundai Sonata	t	2022-08-07 21:32:37+00	3
1	C1	Mercedes Benz	t	2022-08-07 21:29:22+00	1
2	C2	Mercedes Benz	t	2022-08-07 21:29:31+00	1
10	C10	CTX-10	t	2022-08-10 06:11:20+00	1
11	C11	CTX-11	t	2022-08-10 06:12:11+00	1
12	C12	CRX-FN-11	t	2022-08-10 06:12:28+00	1
13	C13	CRX-FN-13	t	2022-08-10 06:12:35+00	1
14	C14	CRX-FN-14	t	2022-08-10 06:12:39+00	1
15	C15	CRX-FN-15	t	2022-08-10 06:12:50+00	2
16	C16	CRX-FN-16	t	2022-08-10 06:12:54+00	2
17	C17	CRX-FN-17	t	2022-08-10 06:12:56+00	2
18	C18	CRX-FN-18	t	2022-08-10 06:12:59+00	2
19	C19	CRX-FN-19	t	2022-08-10 06:13:03+00	2
20	C20	Hyundai CRX-FN-20	t	2022-08-10 06:13:33+00	3
21	C21	Hyundai CRX-FN-21	t	2022-08-10 06:13:36+00	3
22	C22	Hyundai CRX-FN-22	t	2022-08-10 06:13:39+00	3
23	C23	Hyundai CRX-FN-23	t	2022-08-10 06:13:42+00	3
24	C24	Hyundai CRX-FN-24	t	2022-08-10 06:13:45+00	3
25	C25	Hyundai CRX-FN-25	t	2022-08-10 06:13:53+00	3
26	C26	Hyundai GT-CS-26	t	2022-08-10 06:14:04+00	3
27	C27	Hyundai GT-CS-27	t	2022-08-10 06:14:08+00	3
28	C28	Hyundai GT-CSX-28	f	2022-08-10 06:24:15+00	3
\.


--
-- Data for Name: cars_makers; Type: TABLE DATA; Schema: public; Owner: carreservationadmin
--

COPY public.cars_makers (car_maker_id, car_maker_name) FROM stdin;
1	Mercedes
2	BMW
3	Hyundai
4	Honda
\.


--
-- Data for Name: reserved_cars; Type: TABLE DATA; Schema: public; Owner: carreservationadmin
--

COPY public.reserved_cars (reserved_car_id, car_id, user_id, reservation_date, reservation_minutes) FROM stdin;
1	1	1	2022-10-07 23:47:25+00	60
2	2	1	2022-10-07 23:47:42+00	60
3	3	1	2022-10-07 23:47:50+00	60
4	4	1	2022-10-07 23:47:56+00	60
5	2	1	2022-10-07 23:48:11+00	60
6	1	2	2022-10-07 23:48:20+00	60
7	2	2	2022-10-07 23:48:25+00	60
8	3	2	2022-10-07 23:48:32+00	60
9	4	2	2022-10-07 23:48:41+00	60
10	1	1	2022-08-10 06:36:34+00	60
11	1	1	2022-08-10 06:40:58+00	60
12	1	1	2022-08-10 06:41:25+00	60
13	1	1	2022-08-10 06:46:41+00	60
14	1	1	2022-08-10 07:02:45+00	60
15	1	1	2022-08-10 07:03:39+00	60
16	1	1	2022-08-10 07:05:35+00	60
17	1	1	2022-08-10 07:13:15+00	30
18	1	1	2022-08-10 08:24:04+00	60
19	1	1	2022-08-10 08:33:58+00	60
20	1	1	2022-08-10 08:33:58+00	60
21	1	1	2022-08-10 08:33:58+00	60
22	1	1	2022-08-10 08:33:58+00	60
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: carreservationadmin
--

COPY public.users (user_id, user_name, login, password) FROM stdin;
1	admin	admin	admin12345
2	moderator	moderator	moderator12345
\.


--
-- Name: cars_car_id_seq; Type: SEQUENCE SET; Schema: public; Owner: carreservationadmin
--

SELECT pg_catalog.setval('public.cars_car_id_seq', 28, true);


--
-- Name: reserved_cars_reserved_car_id_seq; Type: SEQUENCE SET; Schema: public; Owner: carreservationadmin
--

SELECT pg_catalog.setval('public.reserved_cars_reserved_car_id_seq', 22, true);


--
-- Name: cars cars_models_pkey; Type: CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_models_pkey PRIMARY KEY (car_id);


--
-- Name: cars_makers cars_models_pkey1; Type: CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.cars_makers
    ADD CONSTRAINT cars_models_pkey1 PRIMARY KEY (car_maker_id);


--
-- Name: reserved_cars reserved_cars_pkey; Type: CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.reserved_cars
    ADD CONSTRAINT reserved_cars_pkey PRIMARY KEY (reserved_car_id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- Name: cars cars_cars_makers_fk; Type: FK CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_cars_makers_fk FOREIGN KEY (maker_id) REFERENCES public.cars_makers(car_maker_id) NOT VALID;


--
-- Name: reserved_cars cars_reservations_fk; Type: FK CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.reserved_cars
    ADD CONSTRAINT cars_reservations_fk FOREIGN KEY (car_id) REFERENCES public.cars(car_id);


--
-- Name: reserved_cars users_reservations_fk; Type: FK CONSTRAINT; Schema: public; Owner: carreservationadmin
--

ALTER TABLE ONLY public.reserved_cars
    ADD CONSTRAINT users_reservations_fk FOREIGN KEY (user_id) REFERENCES public.users(user_id);


--
-- PostgreSQL database dump complete
--

--
-- Database "postgres" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4 (Debian 14.4-1.pgdg110+1)
-- Dumped by pg_dump version 14.4 (Debian 14.4-1.pgdg110+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE postgres;
--
-- Name: postgres; Type: DATABASE; Schema: -; Owner: carreservationadmin
--

CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE postgres OWNER TO carreservationadmin;

\connect postgres

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: DATABASE postgres; Type: COMMENT; Schema: -; Owner: carreservationadmin
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- PostgreSQL database dump complete
--

--
-- PostgreSQL database cluster dump complete
--

