using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Configuration;


namespace FoaBrugerOprettelse.DomainModels
{
    public class ADPerson
    {
        public bool TjekInitialerAD(string initialer)
        {
            try
            {
                // create LDAP connection object 
                DirectoryEntry ldapEntry = new DirectoryEntry("LDAP://foa.dk/OU=brugere,DC=foa,DC=dk");
                ldapEntry.Username = "LDAPtilidszonen";
                ldapEntry.Password = "LDAPeiss";

                DirectorySearcher dSeacher = new DirectorySearcher(ldapEntry);
                dSeacher.Filter = ("sAMAccountName=" + initialer);
                SearchResultCollection resultCollection = dSeacher.FindAll();


                System.Diagnostics.Debug.Write(resultCollection.Count);

                if (resultCollection.Count == 0) {

                    return true;
                }
                return false;
               
            }
            catch (Exception ex)
            {
                string errormessage = ex.Message + " [External Data - Active Directory - ADfunktionspostkasser]";
                ex.Data.Add(errormessage, ex.Message);
                throw;
            }
          
        }



        public bool TjekOmLoginErLeder()
        {

            try
            {
                // create LDAP connection object 
                DirectoryEntry ldapEntry = new DirectoryEntry("LDAP://foa.dk/OU=brugere,DC=foa,DC=dk");
                ldapEntry.Username = "LDAPtilidszonen";
                ldapEntry.Password = "LDAPeiss";

                

                DirectorySearcher dSeacher = new DirectorySearcher(ldapEntry);
                //dSeacher.Filter = "(&(objectClass=user)(sAMAccountName="+Environment.UserName +")(memberOf=CN=360Leder,OU=brugere,DC=foa,DC=dk))";
                dSeacher.Filter = "(&(objectClass=user)(sAMAccountName=cln001)(memberOf=CN=360Leder,OU=brugere,DC=foa,DC=dk))";
                SearchResultCollection resultCollection = dSeacher.FindAll();


                if (resultCollection.Count == 0)
                {
                    return false;
                }

                return true;


            }
            catch (Exception ex)
            {
                string errormessage = ex.Message + " [External Data - Active Directory - ADfunktionspostkasser]";
                ex.Data.Add(errormessage, ex.Message);
                throw;
            }

        }



        public List<string> Hent360Adgangsgrupper()
        {

            try
            {
               //create LDAP connection object 
                DirectoryEntry ldapEntry = new DirectoryEntry("LDAP://foa.dk/OU=brugere,DC=foa,DC=dk");
                ldapEntry.Username = "LDAPtilidszonen";
                ldapEntry.Password = "LDAPeiss";

                DirectorySearcher dSeacher = new DirectorySearcher(ldapEntry);
                dSeacher.Filter = "(&(objectClass=group)(cn=360*))";
                SearchResultCollection resultCollection = dSeacher.FindAll();

                List<string> grupper = new List<string>();


                foreach (SearchResult result in resultCollection)
                {

                    foreach (string gruppe in result.Properties["name"])
                    {

                        grupper.Add(gruppe);

                    }
                }


                return grupper;

            }
            catch (Exception ex)
            {
                string errormessage = ex.Message + " [External Data - Active Directory - ADfunktionspostkasser]";
                ex.Data.Add(errormessage, ex.Message);
                throw;
            }

        }



        public List<string> Hent360Adgangsgrupper(string initialer)
        {

            try
            {
                // create LDAP connection object 
                DirectoryEntry ldapEntry = new DirectoryEntry("LDAP://foa.dk/OU=brugere,DC=foa,DC=dk");
                ldapEntry.Username = "LDAPtilidszonen";
                ldapEntry.Password = "LDAPeiss";

                DirectorySearcher dSeacher = new DirectorySearcher(ldapEntry);
                dSeacher.Filter = "(&(objectClass=user)(sAMAccountName=" + initialer + "))";
                SearchResultCollection resultCollection = dSeacher.FindAll();

                List<string> grupper = new List<string>();

                foreach (SearchResult result in resultCollection)
                {

                    foreach (string gruppe in result.Properties["memberOf"])
                    {
                        if (gruppe.Contains("360"))
                        {
                            grupper.Add(gruppe);
                        }
                    }
                }


                return grupper;

            }
            catch (Exception ex)
            {
                string errormessage = ex.Message + " [External Data - Active Directory - ADfunktionspostkasser]";
                ex.Data.Add(errormessage, ex.Message);
                throw;
            }

        }


        public List<string> HentFunktionsPostkasser(int område, string afdeling)
        {
            System.Diagnostics.Debug.Write(område);

            try
            {
                // create LDAP connection object 
                DirectoryEntry ldapEntry = new DirectoryEntry("LDAP://foa.dk/OU=funktionspostkasser,DC=foa,DC=dk");
                ldapEntry.Username = "LDAPtilidszonen";
                ldapEntry.Password = "LDAPeiss";

                DirectorySearcher dSeacher = new DirectorySearcher(ldapEntry);
                switch (område)
                {
                    case 1:
                        //A-kasse
                        dSeacher.Filter = "(department=" + afdeling + ")";
                        break;
                    case 2:
                        // Faglig
                        dSeacher.Filter = "(department~=" + afdeling + ")";
                        if (dSeacher.FindAll().Count == 0)
                        {

                            dSeacher.Filter = "(department~=" + (afdeling.Split(' '))[1] + ")";
                        }
                        break;
                    case 3:
                        // Stauning
                        dSeacher.Filter = "(|(department=stauning*)(department =Stauning*))";
                        break;


                }

                SearchResultCollection resultCollection = dSeacher.FindAll();

                List<string> postkasser = new List<string>();

                foreach (SearchResult result in resultCollection)
                {

                    foreach (string gruppe in result.Properties["displayname"])
                    {
                            postkasser.Add(gruppe);
                        
                    }
                }


                return postkasser;

            }
            catch (Exception ex)
            {
                string errormessage = ex.Message + " [External Data - Active Directory - ADfunktionspostkasser]";
                ex.Data.Add(errormessage, ex.Message);
                throw;
            }

        
        }









      
    }
}