PGDMP     #                     z            carsreservation    14.4 (Debian 14.4-1.pgdg110+1)    14.5                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16384    carsreservation    DATABASE     c   CREATE DATABASE carsreservation WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';
    DROP DATABASE carsreservation;
                carsreservationadmin    false            �            1259    16388    cars    TABLE       CREATE TABLE public.cars (
    car_id integer NOT NULL,
    serial_number character varying(50) NOT NULL,
    model character varying(50) NOT NULL,
    status boolean NOT NULL,
    insert_date_time timestamp(0) with time zone NOT NULL,
    maker_id integer NOT NULL
);
    DROP TABLE public.cars;
       public         heap    carsreservationadmin    false            �            1259    16391    cars_car_id_seq    SEQUENCE     �   ALTER TABLE public.cars ALTER COLUMN car_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.cars_car_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          carsreservationadmin    false    209            �            1259    16392    cars_makers    TABLE     z   CREATE TABLE public.cars_makers (
    car_maker_id integer NOT NULL,
    car_maker_name character varying(50) NOT NULL
);
    DROP TABLE public.cars_makers;
       public         heap    carsreservationadmin    false            �            1259    16395    reserved_cars    TABLE        CREATE TABLE public.reserved_cars (
    reserved_car_id integer NOT NULL,
    car_id integer NOT NULL,
    user_id integer NOT NULL,
    reservation_date_time timestamp(0) with time zone NOT NULL,
    reservation_minutes integer NOT NULL,
    is_reserved boolean DEFAULT true NOT NULL
);
 !   DROP TABLE public.reserved_cars;
       public         heap    carsreservationadmin    false            �            1259    16398 !   reserved_cars_reserved_car_id_seq    SEQUENCE     �   ALTER TABLE public.reserved_cars ALTER COLUMN reserved_car_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.reserved_cars_reserved_car_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          carsreservationadmin    false    212            �            1259    16399    users    TABLE     �   CREATE TABLE public.users (
    user_id integer NOT NULL,
    user_name character varying(100) NOT NULL,
    login character varying(100) NOT NULL,
    password character varying(100) NOT NULL
);
    DROP TABLE public.users;
       public         heap    carsreservationadmin    false            	          0    16388    cars 
   TABLE DATA           `   COPY public.cars (car_id, serial_number, model, status, insert_date_time, maker_id) FROM stdin;
    public          carsreservationadmin    false    209   �                 0    16392    cars_makers 
   TABLE DATA           C   COPY public.cars_makers (car_maker_id, car_maker_name) FROM stdin;
    public          carsreservationadmin    false    211   =!                 0    16395    reserved_cars 
   TABLE DATA           �   COPY public.reserved_cars (reserved_car_id, car_id, user_id, reservation_date_time, reservation_minutes, is_reserved) FROM stdin;
    public          carsreservationadmin    false    212   u!                 0    16399    users 
   TABLE DATA           D   COPY public.users (user_id, user_name, login, password) FROM stdin;
    public          carsreservationadmin    false    214   �!                  0    0    cars_car_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.cars_car_id_seq', 21, true);
          public          carsreservationadmin    false    210                       0    0 !   reserved_cars_reserved_car_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.reserved_cars_reserved_car_id_seq', 3, true);
          public          carsreservationadmin    false    213            r           2606    16406    cars_makers cars_makers_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.cars_makers
    ADD CONSTRAINT cars_makers_pkey PRIMARY KEY (car_maker_id);
 F   ALTER TABLE ONLY public.cars_makers DROP CONSTRAINT cars_makers_pkey;
       public            carsreservationadmin    false    211            n           2606    16410    cars cars_model_uk 
   CONSTRAINT     V   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_model_uk UNIQUE (serial_number);
 <   ALTER TABLE ONLY public.cars DROP CONSTRAINT cars_model_uk;
       public            carsreservationadmin    false    209            p           2606    16418    cars cars_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT cars_pkey PRIMARY KEY (car_id);
 8   ALTER TABLE ONLY public.cars DROP CONSTRAINT cars_pkey;
       public            carsreservationadmin    false    209            t           2606    16408     reserved_cars reserved_cars_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.reserved_cars
    ADD CONSTRAINT reserved_cars_pkey PRIMARY KEY (reserved_car_id);
 J   ALTER TABLE ONLY public.reserved_cars DROP CONSTRAINT reserved_cars_pkey;
       public            carsreservationadmin    false    212            v           2606    16416    users user_login_uk 
   CONSTRAINT     O   ALTER TABLE ONLY public.users
    ADD CONSTRAINT user_login_uk UNIQUE (login);
 =   ALTER TABLE ONLY public.users DROP CONSTRAINT user_login_uk;
       public            carsreservationadmin    false    214            x           2606    16414    users user_name_uk 
   CONSTRAINT     R   ALTER TABLE ONLY public.users
    ADD CONSTRAINT user_name_uk UNIQUE (user_name);
 <   ALTER TABLE ONLY public.users DROP CONSTRAINT user_name_uk;
       public            carsreservationadmin    false    214            z           2606    16412    users users_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            carsreservationadmin    false    214            |           2606    16424 )   reserved_cars car_id_reserved_car_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reserved_cars
    ADD CONSTRAINT car_id_reserved_car_id_fkey FOREIGN KEY (car_id) REFERENCES public.cars(car_id) NOT VALID;
 S   ALTER TABLE ONLY public.reserved_cars DROP CONSTRAINT car_id_reserved_car_id_fkey;
       public          carsreservationadmin    false    212    3184    209            {           2606    16419    cars maker_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.cars
    ADD CONSTRAINT maker_id_fkey FOREIGN KEY (maker_id) REFERENCES public.cars_makers(car_maker_id) NOT VALID;
 <   ALTER TABLE ONLY public.cars DROP CONSTRAINT maker_id_fkey;
       public          carsreservationadmin    false    211    3186    209            }           2606    16429 +   reserved_cars user_id_reserved_uder_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reserved_cars
    ADD CONSTRAINT user_id_reserved_uder_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) NOT VALID;
 U   ALTER TABLE ONLY public.reserved_cars DROP CONSTRAINT user_id_reserved_uder_id_fkey;
       public          carsreservationadmin    false    3194    214    212            	   I  x�m�=O�0@g�WxGF�s�$��	J%(��PU,Q�����2���D�����=�엀"P��a�o��~�w?��"��4���X�;k��Ӛ�Uf4����p���X��`�TT^�i��n�鎧� iX�W�o�
N�]�(��B�a�+E���c#�YjEuƃ�#|���x�x���݉���ݶ�ԴX���Aa&�_:,x�UsG��s��pop#G�-�x���P�!FH�����/δ5�4$ŝ�!W*I)CR�9Te�~\L$ޟo��C}�go�ٻ���I��&3�d����x-���S'	���1��O� �EQ�����         (   x�3��M-JNMI-�2���,�KI��2���2�b���� ��	\         D   x��ʱ�@�ڙ�=r����5����
]{��`���ʖVI\&�g�;�޳�zq������         /   x�3�LL��̃��F�&�HL.#����ԢĒ�"�
������ ���     