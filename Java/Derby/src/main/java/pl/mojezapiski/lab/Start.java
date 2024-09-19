package pl.mojezapiski.lab;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

/**
 *
 * @author pawel
 */
public class Start {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        System.out.println("Zaczynamy:");
        try {
            Class.forName("org.apache.derby.jdbc.EmbeddedDriver"); // rejestracja sterownika
            Connection c = DriverManager.getConnection("jdbc:derby:derbylab;create=true", "java", "java");// utworzenie polaczenia

            Statement s = c.createStatement();
            s.execute("CREATE TABLE LAB01.PRAC(NAME varchar(255), ID int)");
            s.close();
            
            s = c.createStatement();
            s.execute("INSERT INTO LAB01.PRAC(NAME, ID) VALUES ('AAA', 1)");
            s.execute("INSERT INTO LAB01.PRAC(NAME, ID) VALUES ('BBB', 2)");
            s.execute("INSERT INTO LAB01.PRAC(NAME, ID) VALUES ('CCC', 3)");
            s.close();
             
            s = c.createStatement(); // Stworzenie zapytania
            ResultSet rs = s.executeQuery("SELECT * FROM LAB01.PRAC "); // Wykonanie zapytania SELECT * FROM LAB01.PRAC

            while (rs.next()) //petla po wszystkich wierszach wyniku
                System.out.println("Imie = " + rs.getString("NAME") + ", ID = " + rs.getInt("ID")); // pobranie i wyświetlenie wartosci kolumny IMIE i ID

            s.close(); // Zamkniecie zapytania...
            c.close(); // ...i połączenia.

            System.exit(0);
        } catch (SQLException | ClassNotFoundException sqlE) {
            System.out.println("Wyjątek : " + sqlE.getMessage());
            System.exit(0);
        }
    }
};
