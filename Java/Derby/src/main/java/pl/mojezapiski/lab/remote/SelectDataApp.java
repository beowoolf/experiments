package pl.mojezapiski.lab.remote;

import java.sql.*;

public class SelectDataApp {

    public SelectDataApp() {
    }

    public void selectData() {
        Connection con = null;
        try {
            // Ładujemy plik z klasą sterownika bazy danych
            Class.forName("org.apache.derby.jdbc.ClientDriver");
            // Tworzymy połączenie do bazy danych
            con = DriverManager.getConnection("jdbc:derby://localhost:1527/lab", "lab", "lab");
            // Tworzymy obiekt wyrażenia
            Statement statement = con.createStatement();
            // Wysyłamy zapytanie do bazy danych
            ResultSet rs = statement.executeQuery("SELECT * FROM Dane");
            // Przeglądamy otrzymane wyniki
            System.out.printf("|%-3s|%-12s|%-10s|%-5s|\n", "ID", "Nazwisko", "Imię", "Ocena");
            System.out.println("-----------------------------------");
            while (rs.next()) {
                for( int i = 1;i<=rs.getMetaData().getColumnCount();i++) {
                //System.out.printf("|%-3s", rs.getInt("id"));
                //System.out.printf("|%-12s", rs.getString("nazwisko"));
                //System.out.printf("|%-10s", rs.getString("imie"));
                //System.out.printf("| %-4s|\n", rs.getFloat("ocena"));
                    System.out.printf("|%-12s", rs.getString(rs.getMetaData().getColumnName(i)));
                }
                System.out.println();
                
            }
            
            System.out.println("-----------------------------------");
            rs.close();
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
        SelectDataApp selectDataApp = new SelectDataApp();
        selectDataApp.selectData();
    }
}
