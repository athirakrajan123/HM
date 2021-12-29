﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace HOSPITALMANAGEMENTSYSTEM.Models
{
    public class PatientOperations
    {
        MySqlConnection con = null;
        public PatientOperations()
        {
            con = new MySqlConnection("server=localhost;database=HospitalManagement;userid=root;password=8606570966");
        }
        public DataSet logincheck(string uname, string pwd)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter madpt = new MySqlDataAdapter("select * from Patients where email='" + uname + "' and password='" + pwd + "'", con);
                madpt.Fill(ds, "doc");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public DataSet ViewProfile(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter adpt = new MySqlDataAdapter("select * from Patients where PatId='" + id + "'", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public bool UpdateProfile(Patients d)
        {
            bool b = false;
            try
            {
                MySqlDataAdapter adpt = new MySqlDataAdapter("update Patients set PatName='" + d.PatName + "',Address='" + d.Address + "',bloodgrp='" +d.bloodgrp+ "',phonenumber='" + d.phonenumber + "',age='" + d.age + "',email='" + d.email + "',password='" + d.password + "' where PatId='" + d.PatId + "'", con);
                DataSet ds = new DataSet();
                adpt.Fill(ds, "apt");
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;
        }
        public bool EditProfile(string id, PatientDemo p)
        {
            Patients a = new Patients();
            a.PatId = id;
            a.PatName = p.PatName;
            a.Address = p.Address;
            a.bloodgrp = p.bloodgrp;
            a.phonenumber = p.phonenumber;
            a.age = p.age;
            a.email = p.email;
            a.password = p.password;
            bool b = UpdateProfile(a);
            return b;
        }
        public DataSet ViewAllDoctors()
        {
            MySqlDataAdapter adpt = new MySqlDataAdapter("select d.DoctId,d.DoctName,d.Gender,d.Address,d.phonenumber,d.email,s.specializationName from Doctors as d ,Specialization as s where d.spclId=s.spclId", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "doc");
            return ds;
        }
        public DataSet ViewAppointment(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter adpt = new MySqlDataAdapter("select * from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p inner join Specialization as s on a.PatId=p.PatId and d.spclId=s.spclId  where a.PatId='" + id + "' and a.flag=0", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public DataSet ViewPatient(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter adpt = new MySqlDataAdapter("select * from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p inner join Specialization as s on a.PatId=p.PatId and d.spclId=s.spclId  where a.PatId='" + id + "' and a.flag=1", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public DataSet PatientPrescriptions(string id, string aid)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter adpt = new MySqlDataAdapter("select * from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p inner join Specialization as s on a.PatId=p.PatId and d.spclId=s.spclId where a.PatId='" + id + "' and a.AppointmentId='" + aid + "' and a.flag=1", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
    }
}