package pl.mojezapiski.lab.remote;

import java.sql.*;

public class CreateTablesApp {

    public void createTables() {
        Connection con = null;
        try {
            // Ładujemy plik z klasą sterownika bazy danych
            Class.forName("org.apache.derby.jdbc.ClientDriver");
            // Tworzymy połączenie do bazy danych
            con = DriverManager.getConnection("jdbc:derby://localhost:1527/lab", "lab", "lab");
            ///Test czy onajuż istnieje czy ni :DDDDDDDDD + dodanie if i printa do wypisania tego
            ResultSet rs = con.getMetaData().getTables(null, null, "DANE", null);
            if(rs.next()==false) 
            {
                // Tworzymy obiekt wyrażenia
                Statement statement = con.createStatement();
                // Tworzymy pola tabeli
                statement.executeUpdate("CREATE TABLE Dane " +
                        "(id INTEGER, nazwisko VARCHAR(50), " +
                        "imie VARCHAR(50), ocena FLOAT )");
                System.out.println("Table created");
            } else
                System.out.println("Table not created");
        } catch (SQLException sqle) {
            System.err.println("SQL exception: " + sqle.getMessage());
        } catch (ClassNotFoundException cnfe) {
            System.err.println("ClassNotFound exception: " + cnfe.getMessage());
        } catch (Exception e) {
            System.err.println("Another exception: " + e.getMessage());
        } finally {
            try {
                if (con != null) {
                    con.close();
                }
            } catch (SQLException sqle) {
                System.err.println("SQL exception: " + sqle.getMessage());
            }
        }
    }

    public static void main(String[] args) {
        CreateTablesApp createTablesApp = new CreateTablesApp();
        createTablesApp.createTables();
    }
}
